using medexnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using DataLibrary.BusinessLogic;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;

namespace medexnet.Controllers
{
    public class PatientController : Controller
    {
        public static UserModel currentPatient = new UserModel();
        public static List<Notification> notificationList = new List<Notification>();
        public static int chatID = -1;
        public static int notifyCounter = 1;
        public ActionResult Index(UserModel patient)
        {
            currentPatient = GetInfo(patient);

            return View(currentPatient);
        }
        public UserModel GetInfo(UserModel temp)
        {
            List<DataLibrary.Models.PatientPrescriptions> data = PatientProcessor.LoadPatientPrescriptions(temp.Id);
            List<PatientPrescriptions> patientPrescriptions = new List<PatientPrescriptions>();
            patientPrescriptions = data.ConvertAll(new Converter<DataLibrary.Models.PatientPrescriptions, PatientPrescriptions>(DALtoMedex.DMPatientPrescriptions));
            temp.SetPrescriptions(patientPrescriptions);
            foreach (PatientPrescriptions p in temp.myPrescriptions)
            {
                p.dDates = PatientProcessor.loadPrescriptionDelivery(p.Id).ConvertAll(new Converter<DataLibrary.Models.Delivery, Delivery>(DALtoMedex.DMDeliveries));
            }
            temp.myAppointments = PatientProcessor.loadAppointmentData(temp.Id).ConvertAll(new Converter<DataLibrary.Models.Appointment, Appointment>(DALtoMedex.DMAppointmentData));
            temp.myChats = PatientProcessor.loadChats(temp.Id).ConvertAll(new Converter<DataLibrary.Models.Chats, Chats>(DALtoMedex.DMChatData));
            temp.currentChatID = chatID;
            temp.myNotifications = notificationList;
            temp.myDoctors = PatientProcessor.loadDoctorData(temp.Id).ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DALtoMedex.DMDoctorData));
            return temp;
        }
        public UserModel GetPatientInfo(UserModel temp)
        {
            List<DataLibrary.Models.UserModel> data = Processor.LoadUser(currentPatient.email, currentPatient.password);
            List<UserModel> userList = new List<UserModel>();
            userList = data.ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DALtoMedex.GetUserData));
            temp = userList[0];

            return temp;
        }
        public ActionResult Prescriptions(UserModel patient)
        {
            currentPatient = GetInfo(patient);
            currentPatient.myNotifications.RemoveAll(x => x.type == Notification.NotificationType.prescription);

            return View(currentPatient);
        }

        public ActionResult Deliveries(UserModel patient)
        {
            currentPatient = GetInfo(patient);
            currentPatient.myNotifications.RemoveAll(x => x.type == Notification.NotificationType.delivery);
            return View(currentPatient);
        }

        public ActionResult DoctorInfo(UserModel patient)
        {
            currentPatient = GetInfo(patient);
            return View(currentPatient);
        }

        private JsonResult LoadDoctorData()
        {
            JsonResult Jason = new JsonResult();
            List<UserModel> tempDoctorList = PatientProcessor.loadDoctorData(currentPatient.Id).ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DALtoMedex.DMDoctorData)); ;

            Jason = this.Json(tempDoctorList, JsonRequestBehavior.AllowGet);

            return Jason;
        }
        public ActionResult Appointments(UserModel patient)
        {
            currentPatient = GetInfo(patient);
            currentPatient.myNotifications.RemoveAll(x => x.type == Notification.NotificationType.appointment);
            return View(currentPatient);
        }
        public ActionResult MessageInbox(UserModel patient)
        {
            currentPatient = GetPatientInfo(patient);
            currentPatient = GetInfo(currentPatient);
            currentPatient.myNotifications.RemoveAll(x => x.type == Notification.NotificationType.message);

            return View(currentPatient);
        }
        public ActionResult Settings(UserModel patient)
        {
            currentPatient = GetPatientInfo(patient);
            currentPatient = GetInfo(currentPatient);
            return View(currentPatient);
        }

        public ActionResult ChangeSettings(UserModel patient)
        {
            currentPatient = GetInfo(patient);
            return View(currentPatient);
        }

        #region Partial Views

        [HttpGet]
        public ActionResult getPrescriptionData()
        {
            return PartialView("PrescriptionsPartialView", currentPatient);
        }
        
        [HttpGet]
        public ActionResult ChatPartial()
        {
            return PartialView("ChatPartialView", currentPatient);
        }
        #endregion

        #region notifications

        public List<Notification> getMessageNotifications(List<Notification> notify)
        {
             List<Chats> tempChatList = PatientProcessor.loadChats(currentPatient.Id).ConvertAll(new Converter<DataLibrary.Models.Chats, Chats>(DALtoMedex.DMChatData));
            foreach (Chats c in tempChatList)
            {
                if(!currentPatient.myChats.Exists(x => x.Id == c.Id))
                {
                    notify.Add(new Notification() {id = notifyCounter++, description = "New Chat from Dr. " + currentPatient.myDoctors.Find(d => d.Id == c.doctorID).lName, time = DateTime.Now.ToString("ss") + " seconds ago...", type = Notification.NotificationType.message, image = "ti-envelope", address = "/patient/messageinbox/" + currentPatient.Id + "?" });
                    currentPatient.myChats.Add(c);
                }
                else
                {
                    foreach (Message m in c.messageList)
                    {
                        Chats tChat = currentPatient.myChats.Find(x => x.Id == c.Id);
                        if (!tChat.messageList.Exists(x => x.Id == m.Id))
                        {
                            notify.Add(new Notification() { id = notifyCounter++, description = "New Message in " + c.topic, time = DateTime.Now.ToString("ss") + " seconds ago...", type = Notification.NotificationType.message, image = "ti-envelope", address = "/patient/messageinbox/" + currentPatient.Id + "?" });
                            currentPatient.myChats.Find(x => x.Id == c.Id).messageList = c.messageList;
                        }
                    }
                }
            }
            return notify;
        }
        public List<Notification> getAppointmentNotifications(List<Notification> notify)
        {
            List<Appointment> tempAppList = PatientProcessor.loadAppointmentData(currentPatient.Id).ConvertAll(new Converter<DataLibrary.Models.Appointment, Appointment>(DALtoMedex.DMAppointmentData));
            foreach (Appointment a in tempAppList)
            {
               if(currentPatient.myAppointments.Exists(x => x.Id == a.Id))
                {
                    Appointment temp = currentPatient.myAppointments.Find(x => x.Id == a.Id);
                    if(temp.isConfirmed != a.isConfirmed)
                    {
                        notify.Add(new Notification() { id = notifyCounter++, description = "Appointment " + a.desc + " Confirmed.", time = DateTime.Now.ToString("ss") + " seconds ago...",type = Notification.NotificationType.appointment });
                        temp.isConfirmed = a.isConfirmed;
                    }
                }
            }
            return notify;
        }      
        
        public List<Notification> getPrescriptionNotifications(List<Notification> notify)
        {
            List<PatientPrescriptions> tempPrescriptions = PatientProcessor.LoadPatientPrescriptions(currentPatient.Id).ConvertAll(new Converter<DataLibrary.Models.PatientPrescriptions, PatientPrescriptions>(DALtoMedex.DMPatientPrescriptions));
            foreach (PatientPrescriptions p in tempPrescriptions)
            {
               if(!currentPatient.myPrescriptions.Exists(x => x.Id == p.Id))
                {
                    notify.Add(new Notification() { id = notifyCounter++, description = "New Prescription from" + currentPatient.myDoctors.Find(d => d.Id == p.doctorFID).lName, time = DateTime.Now.ToString("ss") + " seconds ago...", type = Notification.NotificationType.prescription, image = "ti-prescription-bottle", address = "/patient/Prescriptions/" + currentPatient.Id + "?" });
                    currentPatient.myPrescriptions.Add(p);
                }
            }
            notify = getDeliveryNotifications(notify, tempPrescriptions);
            return notify;
        }  
        
        public List<Notification> getDeliveryNotifications(List<Notification> notify, List<PatientPrescriptions> tempPrescriptions)
        {
            //foreach (PatientPrescriptions p in tempPrescriptions)
            //{
            //    p.dDates = PatientProcessor.loadPrescriptionDelivery(p.Id).ConvertAll(new Converter<DataLibrary.Models.Delivery, Delivery>(DALtoMedex.DMDeliveries));
            //}
            //foreach (Delivery d in )
            //{
            //   if(currentPatient.myAppointments.Exists(x => x.Id == a.Id))
            //    {
            //        Appointment temp = currentPatient.myAppointments.Find(x => x.Id == a.Id);
            //        if(temp.isConfirmed != a.isConfirmed)
            //        {
            //            notify.Add(new Notification() { id = notifyCounter++, description = "Appointment " + a.desc + " Confirmed.", time = DateTime.Now.ToString("ss") + " seconds ago...",type = Notification.NotificationType.appointment });
            //            temp.isConfirmed = a.isConfirmed;
            //        }
            //    }
            //}
            return notify;
        }

        [HttpGet]
        public JsonResult GetNotifications()
        {
            List<Notification> lstDataSubmit = currentPatient.myNotifications;
            lstDataSubmit = getMessageNotifications(lstDataSubmit);
            lstDataSubmit = getAppointmentNotifications(lstDataSubmit);

            currentPatient.myNotifications = lstDataSubmit;
            notificationList = lstDataSubmit;
            return Json(lstDataSubmit, JsonRequestBehavior.AllowGet);
        }

       
        #endregion
       
        
        #region Calendar Methods
        private List<CalendarEvent> LoadAppointmentData()
        {
            List<CalendarEvent> temp = new List<CalendarEvent>();
            foreach (Appointment item in currentPatient.myAppointments)
            {
                temp.Add(new CalendarEvent { Sr = 4, Title = item.desc, Start_Date = Convert.ToDateTime(item.date).ToShortDateString(), End_Date = Convert.ToDateTime(item.date).ToShortDateString(), Desc = "Appointment", PriorityColor = "#960f2a" });
            }
            return temp;
        }

        public ActionResult GetAppointmentData()
        {
            // Initialization.  
            JsonResult result = new JsonResult();
            try
            {
                // Loading.  
                List<CalendarEvent> data = this.LoadAppointmentData();
                // Processing.  
                result = this.Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Return info.  
            return result;
        }

        private List<CalendarEvent> LoadDeliveryCalendarData()
        {
            List<CalendarEvent> temp = new List<CalendarEvent>();
            foreach (PatientPrescriptions item in currentPatient.myPrescriptions)
            {
                foreach (Delivery d in item.dDates)
                {
                    temp.Add(new CalendarEvent { Sr = 4, Title = item.name + " " + item.dosage + " Shipped", Start_Date = Convert.ToDateTime(d.shippedDate).ToShortDateString(), End_Date = Convert.ToDateTime(d.shippedDate).ToShortDateString(), Desc = "Shipped", PriorityColor = "#1313ad" });
                    temp.Add(new CalendarEvent { Sr = 4, Title = "Delivery Arrives: " + item.name + " " + item.dosage, Start_Date = Convert.ToDateTime(d.arrivalDate).ToShortDateString(), End_Date = Convert.ToDateTime(d.arrivalDate).ToShortDateString(), Desc = "Estimated Delivery Date", PriorityColor = "#1313ad" });
                }
            }
            return temp;
        }

        public ActionResult GetDeliveryData()
        {
            // Initialization.  
            JsonResult result = new JsonResult();
            try
            {
                // Loading.  
                List<CalendarEvent> data = this.LoadDeliveryCalendarData();
                // Processing.  
                result = this.Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Return info.  
            return result;
        }

        public ActionResult GetCalendarData()
        {
            // Initialization.  
            JsonResult result = new JsonResult();
            try
            {
                // Loading.  
                List<CalendarEvent> data = this.LoadCalendarData();
                // Processing.  
                result = this.Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Return info.  
            return result;
        }

        private List<CalendarEvent> LoadCalendarData()
        {
            List<CalendarEvent> temp = new List<CalendarEvent>();
            foreach (PatientPrescriptions item in currentPatient.myPrescriptions)
            {
                for(int i = 0; i < item.pillCount; i++)
                {
                    temp.Add(new CalendarEvent { Sr = 2, Title = "Take " + item.name, Start_Date = Convert.ToDateTime(item.datePrescribed).AddDays(i).ToShortDateString(), End_Date = Convert.ToDateTime(item.datePrescribed).AddDays(i).ToShortDateString(), Desc = item.description, PriorityColor = item.color });
                }
                foreach (Delivery d in item.dDates)
                {
                    temp.Add(new CalendarEvent { Sr = 4, Title = item.name + " " + item.dosage + " Shipped", Start_Date = Convert.ToDateTime(d.shippedDate).ToShortDateString(), End_Date = Convert.ToDateTime(d.shippedDate).ToShortDateString(), Desc = "Shipped", PriorityColor = "#1313ad" });
                    temp.Add(new CalendarEvent { Sr = 4, Title = "Delivery Arrives: " + item.name + " " + item.dosage, Start_Date = Convert.ToDateTime(d.arrivalDate).ToShortDateString(), End_Date = Convert.ToDateTime(d.arrivalDate).ToShortDateString(), Desc = "Estimated Delivery Date", PriorityColor = "#1313ad" });
                }
            }
            foreach (Appointment item in currentPatient.myAppointments)
            {
                temp.Add(new CalendarEvent { Sr = 4, Title = item.desc, Start_Date = Convert.ToDateTime(item.date).ToShortDateString(), End_Date = Convert.ToDateTime(item.date).ToShortDateString(), Desc = "Appointment", PriorityColor = "#960f2a" });
            }
            return temp;
        }

        [HttpPost]
        public ActionResult AddAppointment(int doctorID, string date, string desc)
        {
            if (ModelState.IsValid)
            {
                PatientProcessor.AddAppointment(currentPatient.Id, doctorID, Convert.ToDateTime(date).ToShortDateString(), desc, false);
                string message = "Message has been written.";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }
            return View();
        }
        #endregion

        [HttpPost]
        public ActionResult ChangeChatID(int id, string topic)
        { 
            if (ModelState.IsValid)
            {
                currentPatient.currentChatID = currentPatient.getChatID(currentPatient.Id, id, topic);
                if(currentPatient.currentChatID == -1)
                {
                    PatientProcessor.AddChat(currentPatient.Id, id, topic);
                    GetInfo(currentPatient);
                    currentPatient.currentChatID = currentPatient.getChatID(currentPatient.Id, id, topic);
                }
                chatID = currentPatient.currentChatID;
                string message = "Message has been written.";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }
            return View();
        }

        #region Messaging Methods

        [HttpGet]
        public ActionResult getChatID()
        {
            return Json(new { id = currentPatient.currentChatID, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult MessageChangeChatID(int Id)
        { 
            if (ModelState.IsValid)
            {
                currentPatient.currentChatID = Id;
                chatID = currentPatient.currentChatID;
                string message = "Message has been written.";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddMessage(string msg, int id)
        {
            if (currentPatient.currentChatID == -1)
            {
                string message = "No Valid Chats";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }
            if (ModelState.IsValid)
            {
                PatientProcessor.AddMessage(currentPatient.Id, msg, currentPatient.fName[0] + currentPatient.lName, DateTime.Now.ToShortTimeString(), DateTime.Now.ToShortDateString(), currentPatient.currentChatID);
                string message = "Message has been written.";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }
            return View();
        }

        [HttpGet]
        public ActionResult UpdateChatWindow()
        {
            currentPatient.myChats = PatientProcessor.loadChats(currentPatient.Id).ConvertAll(new Converter<DataLibrary.Models.Chats, Chats>(DALtoMedex.DMChatData));
            return Json(new { Message = "" , JsonRequestBehavior.AllowGet });
        }

        
        #endregion

        #region settings Methods
        [HttpPost]
        public ActionResult ChangeEmail(UserModel newData)
        {
            if (ModelState.IsValid)
            {
                bool validEmail = true;

                if (!newData.email.Contains("@")){validEmail = false;}
                if (!newData.email.Contains(".")){validEmail = false;}

                if (validEmail)
                {
                    PatientProcessor.ChangeEmail(currentPatient.Id, newData.email);
                    currentPatient.email = newData.email;
                    string message = "Your email address has been changed.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
                else
                {
                    string message = "Your email address is not valid.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChangePhone(UserModel newData)
        {
            if (ModelState.IsValid)
            {
                bool validPhone = false;

                if (newData.phoneNumber.Any())
                {
                    validPhone = true;
                }

                if (validPhone)
                {
                    PatientProcessor.ChangePhone(currentPatient.Id, newData.phoneNumber);

                    string message = "Your phone number has been has been changed.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
                else
                {
                    string message = "Your phone number has not been changed.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string cPassword, string nPassword, string cnPassword)
        {
            if(currentPatient.password != cPassword)
            {
                string message = "Your current password is invalid.";
                return Json(new { success = false, Message = message, JsonRequestBehavior.AllowGet });
            }
            if(nPassword != cnPassword)
            {
                string message = "Your new password does not match.";
                return Json(new { success = false, Message = message, JsonRequestBehavior.AllowGet });
            }
            if (ModelState.IsValid)
            {
                PatientProcessor.ChangePassword(currentPatient.Id, nPassword);

                string message = "Your password has been has been changed.";
                return Json(new { success = true, Message = message, JsonRequestBehavior.AllowGet });
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChangeAddress(UserModel newData)
        {
            if (ModelState.IsValid)
            {
                bool validAddress = true;

                if (newData.streetAddress.Length < 4)
                {
                    validAddress = false;
                }
                if (newData.city.Length < 2)
                {
                    validAddress = false;
                }
                if (newData.state.Length < 2)
                {
                    validAddress = false;
                }
                if (newData.zipcode.Length < 5)
                {
                    validAddress = false;
                }

                if (validAddress)
                {
                    PatientProcessor.ChangeAddress(currentPatient.Id, newData.streetAddress, newData.city, newData.state, newData.zipcode);

                    string message = "Your address has been has been changed.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
                else
                {
                    string message = "Your address cant be has be changed.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }

            }
            return View();
        }
        #endregion


        [HttpPost]
        public ActionResult ChangeRxColor(int id, string color)
        {
            if (ModelState.IsValid)
            {
                foreach(PatientPrescriptions p in currentPatient.myPrescriptions)
                {
                    if(p.Id == id)
                    {
                        PatientProcessor.ChangeColor(currentPatient.Id, id, color);
                    }
                }
                string message = "Your Color Changed.";
                return Json(new { success = true, Message = message, JsonRequestBehavior.AllowGet });
            }
            return View();
        }
    }
}