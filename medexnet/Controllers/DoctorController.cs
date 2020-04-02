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
                      
            return View(currentDoctor);
        }       

        public ActionResult Patients()
        {
            List<int> patientIds = DoctorProcessor.GetPatientIds(currentDoctor.Id);
            List<UserModel> patients = new List<UserModel>();

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

        public ActionResult Settings()
        {
            return View(currentDoctor);
        }

        [HttpPost]
        public ActionResult ChangeEmail(UserModel newData)
        {
            if (ModelState.IsValid)
            {
                //CHECK IF EMAIL IS GOOD
                DoctorProcessor.ChangeEmail(currentDoctor.Id, newData.email);

                string message = "You Email has been changed.";
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