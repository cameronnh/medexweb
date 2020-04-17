using medexnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using DataLibrary.BusinessLogic;

namespace medexnet.Controllers
{
    public class DoctorController : Controller
    {
        public static UserModel currentDoctor = new UserModel();

        public ActionResult Index(UserModel doctor)
        {           
            currentDoctor = doctor;
            currentDoctor.myAppointments = DoctorProcessor.loadAppointmentData(currentDoctor.Id).ConvertAll(new Converter<DataLibrary.Models.Appointment, Appointment>(DALtoMedex.DMAppointmentData));
            currentDoctor = getDoctorEvents(currentDoctor);

            return View(currentDoctor);
        }        

        public ActionResult GetCalendarData()
        {          
            JsonResult result = new JsonResult();
            try
            {
                List<CalendarEvent> data = this.LoadCalendarData();
                result = this.Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }          
            return result;
        }

        private List<CalendarEvent> LoadCalendarData()
        {
            List<CalendarEvent> temp = new List<CalendarEvent>();           
            foreach (Appointment item in currentDoctor.myAppointments)
            {
                temp.Add(new CalendarEvent { Sr = 4, Title = item.desc, Start_Date = Convert.ToDateTime(item.date).ToShortDateString(), End_Date = Convert.ToDateTime(item.date).ToShortDateString(), Desc = "Appointment", PriorityColor = "#0271e0" });
            }
            return temp;
        }

        public ActionResult Patients()
        {
            List<int> patientIds = DoctorProcessor.GetPatientIds(currentDoctor.Id);
            List<UserModel> patients = new List<UserModel>();
            //currentDoctor.doctorSelection =

            foreach (int patientId in patientIds)//gets the patient info for all the patients based of id
            {
                List<UserModel> tempPatientData = new List<UserModel>();//(1st list)
                List<DataLibrary.Models.UserModel> tempData = DoctorProcessor.LoadPatientInfo(patientId);//(2nd list) gets data with datamodel
                tempPatientData = tempData.ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DataAccessPatientInfo));//1st list = 2nd list
                UserModel patient = tempPatientData[0];

                patients.Add(patient);
            }
            currentDoctor.SetPatients(patients);

            return View(currentDoctor);
        }

        public UserModel getDoctorSelection(UserModel user)
        {
            List<docPreClasses> classes = new List<docPreClasses>();
            return user;
        }

        public ActionResult Appointments()
        {
            currentDoctor = getUnacceptedAppointments(currentDoctor);
            return View(currentDoctor);
        }

        public UserModel getUnacceptedAppointments(UserModel user)
        {
            List<DoctorEvents> unAcceptedAppointments = new List<DoctorEvents>();

            foreach (medexnet.Models.Appointment item in user.myAppointments)
            {
                if(item.isConfirmed)
                {
                }
                else
                {
                    List<UserModel> tempPatientData = new List<UserModel>();//(1st list)
                    List<DataLibrary.Models.UserModel> tempData = DoctorProcessor.LoadPatientInfo(item.PatientFID);
                    tempPatientData = tempData.ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DataAccessPatientInfo));//1st list = 2nd list
                    UserModel patient = tempPatientData[0];

                    unAcceptedAppointments.Add(new DoctorEvents()
                    {
                        //Date = Convert.ToDateTime(item.date),
                        id = item.Id,
                        Date = item.date,
                        Patient_Name = (patient.fName + " " + patient.lName),
                        Description = item.desc,
                        Type = "Appointment",
                        Patient_Phonenumber = patient.phoneNumber
                    });
                }              
            }
            user.unacceptedAppointments = unAcceptedAppointments;

            return user;
        }

        public ActionResult Messages()
        {
            return View(currentDoctor);
        }

        [HttpPost]
        public ActionResult AddPrescription(PatientPrescriptions pers)
        {
            if (ModelState.IsValid)
            {
                DoctorProcessor.AddPrescription(pers.patientFID, currentDoctor.Id, pers.name, pers.dosage, pers.pillCount, pers.numberofRefills,
                    pers.useBefore, pers.description, DateTime.Now.ToString());

                string message = "Prescription has been written.";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }
            return View();           
        }

        [HttpPost]
        public ActionResult AddAppointment(Appointment app)
        {
            if (ModelState.IsValid)
            {              
                DoctorProcessor.AddAppointment(app.PatientFID, currentDoctor.Id, app.date, app.desc);

                string message = "Appointment has been made.";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddPatientToDoctorRequest(UserModel pat)
        {           
            if (ModelState.IsValid)
            {
                List<DataLibrary.Models.UserModel> data = DoctorProcessor.checkIfPatientExists(pat.fName, pat.lName, pat.email, pat.streetAddress, pat.city, pat.state);
                if (data.Count == 0)
                {
                    string message = "Cannot add this patient. Please check all of the fields.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
                else if (data.Count == 1)
                {
                    List<UserModel> patient = new List<UserModel>();
                    patient = data.ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DALtoMedex.GetUserData));
                    UserModel patientToAdd = patient[0];

                    DoctorProcessor.AddPatient(patientToAdd.Id, currentDoctor.Id);
                }
                else
                {
                    string message = "This patient is already added.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }                         
            }
            return View();
        }
       
        public ActionResult PatientDetails(UserModel currentPatient)
        {         
            List<UserModel> tempPatientData = new List<UserModel>();
            List<DataLibrary.Models.UserModel> tempData = DoctorProcessor.LoadPatientInfo(currentPatient.Id);
            tempPatientData = tempData.ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DataAccessPatientInfo));
            currentPatient = tempPatientData[0];      

            return View(currentPatient);
        }

        public ActionResult PatientsPrescriptions(UserModel currentPatient)
        {                
            List<DataLibrary.Models.PatientPrescriptions> data = PatientProcessor.LoadPatientPrescriptions(currentPatient.Id);
            List<PatientPrescriptions> patientPrescriptions = new List<PatientPrescriptions>();
            patientPrescriptions = data.ConvertAll(new Converter<DataLibrary.Models.PatientPrescriptions, PatientPrescriptions>(DataAccessPatientPerscriptions));
            currentPatient.SetPrescriptions(patientPrescriptions);

            return View(currentPatient);
        }

        public UserModel refreshUserDetails(UserModel user)
        {
            List<DataLibrary.Models.UserModel> data = DoctorProcessor.LoadUser(user.Id);

            List<UserModel> userList = new List<UserModel>();
            userList = data.ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DALtoMedex.GetUserData));

            user = userList[0];

            return user;
        }

        public UserModel getDoctorEvents(UserModel user)
        {
            List<DoctorEvents> eventsList = new List<DoctorEvents>();

            foreach(medexnet.Models.Appointment item in user.myAppointments)
            {
                if (item.isConfirmed)
                {
                    List<UserModel> tempPatientData = new List<UserModel>();//(1st list)
                    List<DataLibrary.Models.UserModel> tempData = DoctorProcessor.LoadPatientInfo(item.PatientFID);
                    tempPatientData = tempData.ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DataAccessPatientInfo));//1st list = 2nd list
                    UserModel patient = tempPatientData[0];

                    eventsList.Add(new DoctorEvents()
                    {
                        //Date = Convert.ToDateTime(item.date),
                        Date = item.date,
                        Patient_Name = (patient.fName + " " + patient.lName),
                        Description = item.desc,
                        Type = "Appointment"
                    });
                }                  
            }
            user.myEvents = eventsList;
            return user;
        }

        public ActionResult Settings()
        {
            currentDoctor = refreshUserDetails(currentDoctor);

            return View(currentDoctor);
        }

        [HttpPost]
        public ActionResult ChangeEmail(UserModel newData)
        {
            if (ModelState.IsValid)
            {
                bool validEmail = true;

                if(newData.email.Contains("@"))
                {
                }
                else
                {
                    validEmail = false;
                }
                if(newData.email.Contains("."))
                {
                }
                else
                {
                    validEmail = false;
                }

                if(validEmail)
                {
                    DoctorProcessor.ChangeEmail(currentDoctor.Id, newData.email);

                    string message = "Your email address has been changed.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
                else
                {
                    string message = "Your email address is not valid.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
            }           
            return View("Settings", "Doctor");
        }

        [HttpPost]
        public ActionResult ChangePhone(UserModel newData)
        {
            if (ModelState.IsValid)
            {
                bool validPhone = true;

                if(newData.phoneNumber.Any())
                {
                    validPhone = false;
                }

                if(validPhone)
                {
                    DoctorProcessor.ChangePhone(currentDoctor.Id, newData.phoneNumber);

                    string message = "Your phone number has been has been changed.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
                else
                {
                    string message = "Your phone number has not been changed.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }                
            }
            return View("Settings", "Doctor");
        }

        [HttpPost]
        public ActionResult ChangePassword(UserModel newData)
        {
            if (ModelState.IsValid)
            {
                DoctorProcessor.ChangePassword(currentDoctor.Id, newData.password);

                string message = "Your password has been has been changed.";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }
            return View("Settings", "Doctor");
        }

        [HttpPost]
        public ActionResult ChangeAddress(UserModel newData)
        {
            if (ModelState.IsValid)
            {
                bool validAddress = true;

                if(newData.streetAddress.Length < 4)
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

                if(validAddress)
                {
                    DoctorProcessor.ChangeAddress(currentDoctor.Id, newData.streetAddress, newData.city, newData.state, newData.zipcode);

                    string message = "Your address has been has been changed.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
                else
                {
                    string message = "Your address cant be has be changed.";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
                
            }
            return View("Settings", "Doctor");
        }

        [HttpPost]
        public ActionResult acceptApp(Appointment app)
        {
            if (ModelState.IsValid)
            {
                DoctorProcessor.acceptApp(app.Id);

                string message = "Appointment has been accepted.";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }

            return View("Appointments", "Doctor");
        }

        [HttpPost]
        public ActionResult declineApp(Appointment app)
        {
            if (ModelState.IsValid)
            {
                DoctorProcessor.declineApp(app.Id);

                string message = "Appointment has been declined.";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }

            return View("Appointments", "Doctor");
        }

        public UserModel PullMessages(UserModel temp)
        {            
            temp.myChats = DoctorProcessor.loadChats(temp.Id).ConvertAll(new Converter<DataLibrary.Models.Chats, Chats>(DALtoMedex.DMChatData));
            temp.currentChatID = currentDoctor.currentChatID;
            return temp;
        }

        public ActionResult MessageInbox(UserModel doctor)
        {
            currentDoctor = PullMessages(doctor);
            if (currentDoctor.currentChatID == -1 && currentDoctor.myChats.Count() > 0)
            {
                currentDoctor.currentChatID = currentDoctor.myChats[0].Id;
            }
            return View(currentDoctor);
        }               

        [HttpPost]
        public ActionResult MessageChangeChatID(int Id)
        {
            if (ModelState.IsValid)
            {
                currentDoctor.currentChatID = Id;
                string message = "Message has been written.";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddMessage(string msg, int id)
        {
            if (currentDoctor.currentChatID == -1)
            {
                string message = "No Valid Chats";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }

            if (ModelState.IsValid)
            {
                PatientProcessor.AddMessage(currentDoctor.Id, msg, currentDoctor.fName[0] + currentDoctor.lName, DateTime.Now.ToShortTimeString(), DateTime.Now.ToShortDateString(), id);
                string message = "Message has been written.";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }
            return View();
        }

        public static UserModel DataAccessPatientInfo(DataLibrary.Models.UserModel temp)
        {
            return new UserModel
            {
                Id = temp.Id,
                fName = temp.fName,
                lName = temp.lName,
                email = temp.email,
                password = temp.password,
                phoneNumber = temp.phoneNumber,
                streetAddress = temp.streetAddress,
                city = temp.city,
                state = temp.state,
                zipcode = temp.zipcode,
                accountType = temp.accountType,
                officeHours = temp.officeHours,
            };
        }

        public static PatientPrescriptions DataAccessPatientPerscriptions(DataLibrary.Models.PatientPrescriptions temp)
        {
            return new PatientPrescriptions
            {
                Id = temp.Id,
                patientFID = temp.patientFID,
                doctorFID = temp.doctorFID,
                prescriptionFID = temp.prescriptionFID,
                name = temp.name,
                dosage = temp.dosage,
                pillCount = temp.pillCount,
                numberofRefills = temp.numberofRefills,
                useBefore = temp.useBefore,
                description = temp.description,
                datePrescribed = temp.datePrescribed
            };
        }       
    }
}