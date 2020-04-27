using medexnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using DataLibrary.BusinessLogic;
using System.Drawing;
//using DataLibrary.Models.DataLibrary.Models.PharmacyPrescriptions;

namespace medexnet.Controllers
{    
    public class PharmacyController : Controller
    {
        public static UserModel currentPacker = new UserModel();
        public static List<PharmacyPrescriptions> allPrescriptions = new List<PharmacyPrescriptions>();
        public ActionResult Index(UserModel packer)
        {
            currentPacker = packer;
        
            return View(currentPacker);
        }

        public ActionResult Inventory()
        {
            List<PharmacyPrescriptions> tempPrescriptions = new List<PharmacyPrescriptions>();
            List<DataLibrary.Models.PharmacyPrescriptions> data = PharmacyProcessor.GetPrescriptionsByPatient();
            tempPrescriptions = data.ConvertAll(new Converter<DataLibrary.Models.PharmacyPrescriptions, PharmacyPrescriptions>(DataAccessPharmacyPrescriptions));
            allPrescriptions = tempPrescriptions;

            return View(allPrescriptions);
        }

        public ActionResult Prescriptions()
        {
            List<PharmacyPrescriptions> tempPrescriptions = new List<PharmacyPrescriptions>();
            List<DataLibrary.Models.PharmacyPrescriptions> data = PharmacyProcessor.GetPrescriptionsByPatient();
            tempPrescriptions = data.ConvertAll(new Converter<DataLibrary.Models.PharmacyPrescriptions, PharmacyPrescriptions>(DataAccessPharmacyPrescriptions));
            allPrescriptions = tempPrescriptions;

            return View(allPrescriptions);
        }

        public static medexnet.Models.PharmacyPrescriptions DataAccessPharmacyPrescriptions(DataLibrary.Models.PharmacyPrescriptions temp)
        {
            return new medexnet.Models.PharmacyPrescriptions
            {
                prescriptionFID = temp.prescriptionFID,
                fName = temp.fName,
                lName = temp.lName,
                prescriptionName = temp.name,
                dosage = temp.dosage,
                pillCount = temp.pillCount,
                numberofRefills = temp.numberofRefills,
                useBefore = temp.useBefore,
                description = temp.description,
                datePrescribed = temp.datePrescribed
            };
        }


        //IM BLOCKING THIS OFF FOR MERGE ERRORS

        public static UserModel currentPatient = new UserModel();

        public static List<PatientPrescriptions> GetUnfilledPrescriptions(int id)
        {
            List<int> sentPrescriptions = PharmacyProcessor.getAlreadyDelPers();
            List<PatientPrescriptions> unfilledPrescriptions = new List<PatientPrescriptions>();

            List<PatientPrescriptions> tempPatientsPrescriptions = new List<PatientPrescriptions>();
            List<DataLibrary.Models.PatientPrescriptions> data = PharmacyProcessor.GetPrescriptionByPatient(id);
            tempPatientsPrescriptions = data.ConvertAll(new Converter<DataLibrary.Models.PatientPrescriptions, PatientPrescriptions>(DataAccessPatientPerscriptions));
            
            foreach(medexnet.Models.PatientPrescriptions item in tempPatientsPrescriptions)
            {
                if(sentPrescriptions.Contains(item.Id))
                {

                }
                else
                {
                    unfilledPrescriptions.Add(item);
                }
            }

            return unfilledPrescriptions;
        }

        public static List<int> GetPatientIds()
        {
            List<int> patientIdsWithUnfilled = new List<int>();
            List<int> patientIds = PharmacyProcessor.getPatientIds();

            foreach(int Id in patientIds)
            {
                List<PatientPrescriptions> patientsUnfilledPrescriptions = GetUnfilledPrescriptions(Id);

                if(patientsUnfilledPrescriptions.Count() == 0)
                {

                }
                else
                {
                    patientIdsWithUnfilled.Add(Id);
                }
            }
            return patientIdsWithUnfilled;
        }

        public UserModel GetPatient()
        {
            List<int> patientIds = GetPatientIds();

            if (patientIds.Count() < 0)
            {
                return null;

            }

            int id = patientIds[0];

            List<UserModel> tempPatientData = new List<UserModel>();
            List<DataLibrary.Models.UserModel> tempData = DoctorProcessor.LoadPatientInfo(id);//(2nd list) gets data with datamodel
            tempPatientData = tempData.ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DataAccessPatientInfo));//1st list = 2nd list
            currentPatient = tempPatientData[0];

            currentPatient.myPrescriptions = GetUnfilledPrescriptions(currentPatient.Id);

            return currentPatient;
        }

        public ActionResult GetData()
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
            foreach (PatientPrescriptions item in currentPatient.myPrescriptions)
            {
                for (int i = 0; i < item.pillCount; i++)
                {
                    temp.Add(new CalendarEvent { Sr = 2, Title = "Take " + item.name, Start_Date = Convert.ToDateTime(item.datePrescribed).AddDays(i).ToShortDateString(), End_Date = Convert.ToDateTime(item.datePrescribed).AddDays(i).ToShortDateString(), Desc = item.description, PriorityColor = "#0271e0" });
                }            
            }           
            return temp;
        }

        public ActionResult PackerCalendar()
        {
            GetPatient();

            return View(currentPatient);
        }

        [HttpPost]
        public ActionResult SetAsDelivered(Delivery del)
        {
            if (ModelState.IsValid)
            {
                foreach(medexnet.Models.PatientPrescriptions item in currentPatient.myPrescriptions)
                {
                    Delivery temp = new Delivery();
                    temp.shippedDate = DateTime.Now.ToString();
                    temp.arrivalDate = DateTime.Now.AddDays(3).ToString();
                    temp.patientFID = currentPatient.Id;
                    temp.Id = item.Id;//used for prescriptions

                    PharmacyProcessor.CreateDelivery(temp.patientFID, temp.Id, temp.shippedDate, temp.arrivalDate);
                }

                string message = "Presciptions have been marked as sent.";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }
            return View();
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

        //
    }
}