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
        public ActionResult Index(UserModel patient)
        {  
            List<DataLibrary.Models.PatientPrescriptions> data = PatientProcessor.LoadPatientPrescriptions(patient.Id);
            List<PatientPrescriptions> patientPrescriptions = new List<PatientPrescriptions>();
            patientPrescriptions = data.ConvertAll(new Converter<DataLibrary.Models.PatientPrescriptions, PatientPrescriptions>(DALToMedex));
            patient.SetPrescriptions(patientPrescriptions);

            return View(patient);
        }
        public static PatientPrescriptions DALToMedex(DataLibrary.Models.PatientPrescriptions temp)
        {
            return new PatientPrescriptions
                {
                Id = temp.Id,
                patientFID = temp.patientFID,
                doctorFID = temp.doctorFID,
                prescriptionFID = temp.prescriptionFID,
                deliveryFID = temp.deliveryFID,
                name = temp.name,
                dosage = temp.dosage,
                pillCount = temp.pillCount,
                numberofRefills = temp.numberofRefills,
                useBefore = temp.useBefore,
                description = temp.description,
                datePrescribed = temp.datePrescribed
            };
        }

        public ActionResult Perscriptions(UserModel patient)
        {
            ViewBag.Message = "Your application description page.";
            List<DataLibrary.Models.PatientPrescriptions> data = PatientProcessor.LoadPatientPrescriptions(patient.Id);
            List<PatientPrescriptions> patientPrescriptions = new List<PatientPrescriptions>();
            patientPrescriptions = data.ConvertAll(new Converter<DataLibrary.Models.PatientPrescriptions, PatientPrescriptions>(DALToMedex));
            patient.SetPrescriptions(patientPrescriptions);
            return View(patient);
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
            temp.Add(new CalendarEvent { Sr = 2, Title = "Take Bumex", Start_Date = DateTime.Today.ToShortDateString(), End_Date = DateTime.Today.AddDays(15).ToShortDateString(), Desc = "Take prescription of Bumex", PriorityColor = "#e9fc0f"});
            temp.Add(new CalendarEvent { Sr = 2, Title = "Take Perc30", Start_Date = DateTime.Today.ToShortDateString(), End_Date = DateTime.Today.AddDays(15).ToShortDateString(), Desc = "Take prescription of perc30", PriorityColor = "#e9fF0f" });

            temp.Add(new CalendarEvent { Sr = 4, Title = "Delivery Arriving", Start_Date = DateTime.Today.AddDays(15).ToShortDateString(), End_Date = DateTime.Today.AddDays(15).ToShortDateString(), Desc = "Estimated delivery arrival today", PriorityColor = "#0cf71c" });
            temp.Add(new CalendarEvent { Sr = 1, Title = "Doctor Appointment", Start_Date = DateTime.Today.AddDays(30).ToShortDateString(), End_Date = DateTime.Today.AddDays(30).ToShortDateString(), Desc = "Doctor appointment today!", PriorityColor = "#f70c0c"});
            return temp;
        }

        public ActionResult Deliveries(UserModel patient)
        {

            return View(patient);
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

        private List<CalendarEvent> LoadDeliveryCalendarData()
        {
            List<CalendarEvent> temp = new List<CalendarEvent>();
            //temp.Add(new CalendarEvent { Sr = 2, Title = "Take Bumex", Start_Date = DateTime.Today.ToShortDateString(), End_Date = DateTime.Today.AddDays(15).ToShortDateString(), Desc = "Take prescription of Bumex", PriorityColor = "#e9fc0f" });
            temp.Add(new CalendarEvent { Sr = 4, Title = "Delivery Arriving", Start_Date = DateTime.Today.AddDays(15).ToShortDateString(), End_Date = DateTime.Today.AddDays(15).ToShortDateString(), Desc = "Estimated delivery arrival today", PriorityColor = "#0cf71c" });
            //temp.Add(new CalendarEvent { Sr = 1, Title = "Doctor Appointment", Start_Date = DateTime.Today.AddDays(30).ToShortDateString(), End_Date = DateTime.Today.AddDays(30).ToShortDateString(), Desc = "Doctor appointment today!", PriorityColor = "#f70c0c" });
            return temp;
        }

        public ActionResult DoctorInfo(UserModel patient)
        {

            return View(patient);
        }
        public ActionResult Appointments(UserModel patient)
        {

            return View(patient);
        }
        public ActionResult MessageInbox(UserModel patient)
        {

            return View(patient);
        }
        public ActionResult Settings(UserModel patient)
        {

            return View(patient);
        }


    }
}