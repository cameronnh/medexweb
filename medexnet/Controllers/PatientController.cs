using medexnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using DataLibrary.BusinessLogic;
using System.Drawing;

namespace medexnet.Controllers
{
    public class PatientController : Controller
    {
        public static UserModel currentPatient = new UserModel();
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

            return temp;
        }

        public ActionResult Perscriptions(UserModel patient)
        {
            currentPatient = GetInfo(patient);
            return View(currentPatient);
        }

        public ActionResult Deliveries(UserModel patient)
        {
            currentPatient = GetInfo(patient);
            return View(currentPatient);
        }

        public ActionResult DoctorInfo(UserModel patient)
        {
            currentPatient = GetInfo(patient);
            currentPatient.myDoctors = PatientProcessor.loadDoctorData(currentPatient.Id).ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DALtoMedex.DMDoctorData)); ;
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
            return View(currentPatient);
        }
        public ActionResult MessageInbox(UserModel patient)
        {
            currentPatient = GetInfo(patient);

            currentPatient.myChats = PatientProcessor.loadChats(currentPatient.Id).ConvertAll(new Converter<DataLibrary.Models.Chats, Chats>(DALtoMedex.DMChatData));
            return View(currentPatient);
        }
        public ActionResult Settings(UserModel patient)
        {
            currentPatient = GetInfo(patient);
            return View(currentPatient);
        }

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
                    temp.Add(new CalendarEvent { Sr = 2, Title = "Take " + item.name, Start_Date = Convert.ToDateTime(item.datePrescribed).AddDays(i).ToShortDateString(), End_Date = Convert.ToDateTime(item.datePrescribed).AddDays(i).ToShortDateString(), Desc = item.description, PriorityColor = "#4a807c" });
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
        public ActionResult AddMessage(string msg, int id)
        {
            if (ModelState.IsValid)
            {
                PatientProcessor.AddMessage(currentPatient.Id, msg, currentPatient.fName[0] + currentPatient.lName, DateTime.Now.ToShortTimeString(), DateTime.Now.ToShortDateString(), id);
                string message = "Message has been written.";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }
            return View();
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
    }
}