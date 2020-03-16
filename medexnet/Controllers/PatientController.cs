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
                p.dDates = PatientProcessor.loadPrescriptionDelivery(p.deliveryFID).ConvertAll(new Converter<DataLibrary.Models.Delivery, Delivery>(DALtoMedex.DMDeliveries));
            }
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
            return View(currentPatient);
        }
        public ActionResult Settings(UserModel patient)
        {
            currentPatient = GetInfo(patient);
            return View(currentPatient);
        }

        private List<CalendarEvent> LoadDeliveryCalendarData()
        {
            List<CalendarEvent> temp = new List<CalendarEvent>();
            foreach (PatientPrescriptions item in currentPatient.myPrescriptions)
            {
                foreach (Delivery d in item.dDates)
                {
                    temp.Add(new CalendarEvent { Sr = 4, Title = item.name + " " + item.dosage + " Shipped", Start_Date = Convert.ToDateTime(d.shippedDate).ToShortDateString(), End_Date = Convert.ToDateTime(d.shippedDate).ToShortDateString(), Desc = "Shipped", PriorityColor = "#0cf71c" });
                    temp.Add(new CalendarEvent { Sr = 4, Title = "Next Delivery: " + item.name + " " + item.dosage, Start_Date = Convert.ToDateTime(d.arrivalDate).ToShortDateString(), End_Date = Convert.ToDateTime(d.arrivalDate).ToShortDateString(), Desc = "Estimated Delivery Date", PriorityColor = "#0cf71c" });
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
                temp.Add(new CalendarEvent { Sr = 2, Title = "Take Bumex", Start_Date = DateTime.Today.ToShortDateString(), End_Date = DateTime.Today.AddDays(15).ToShortDateString(), Desc = "Take prescription of Bumex", PriorityColor = "#e9fc0f" });
                foreach (Delivery d in item.dDates)
                {
                    temp.Add(new CalendarEvent { Sr = 4, Title = item.name + " " + item.dosage + " Shipped", Start_Date = Convert.ToDateTime(d.shippedDate).ToShortDateString(), End_Date = Convert.ToDateTime(d.shippedDate).ToShortDateString(), Desc = "Shipped", PriorityColor = "#0cf71c" });
                    temp.Add(new CalendarEvent { Sr = 4, Title = "Next Delivery: " + item.name + " " + item.dosage, Start_Date = Convert.ToDateTime(d.arrivalDate).ToShortDateString(), End_Date = Convert.ToDateTime(d.arrivalDate).ToShortDateString(), Desc = "Estimated Delivery Date", PriorityColor = "#0cf71c" });
                }
            }

            temp.Add(new CalendarEvent { Sr = 1, Title = "Doctor Appointment", Start_Date = DateTime.Today.AddDays(30).ToShortDateString(), End_Date = DateTime.Today.AddDays(30).ToShortDateString(), Desc = "Doctor appointment today!", PriorityColor = "#f70c0c" });
            return temp;
        }
    }
}