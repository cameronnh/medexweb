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
        public ActionResult Index(UserModel doctor)
        {              
              
            return View(doctor);
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
     
        public ActionResult Patients(UserModel doctor)
        {
            List<int> patientIds = DoctorProcessor.GetPatientIds(doctor.Id);

            List<UserModel> patients = new List<UserModel>();

            foreach (int patientId in patientIds)//gets the patient info for all the patients based of id
            {
                List<UserModel> tempPatientData = new List<UserModel>();//(1st list)
                List<DataLibrary.Models.UserModel> tempData = DoctorProcessor.LoadPatientInfo(patientId);//(2nd list) gets data with datamodel
                tempPatientData = tempData.ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DataAccessPatientInfo));//1st list = 2nd list
                UserModel patient = tempPatientData[0];

                patients.Add(patient);//adds the patient to doctors list of patients
            }

            doctor.SetPatients(patients);//sets the list of patients 
            //maybe i can make a add function         

            return View(doctor);
        }

        public ActionResult PatientDetails(UserModel currentPatient)
        {
            //---------------------- Getting Patient Details Again
            List<UserModel> tempPatientData = new List<UserModel>();
            List<DataLibrary.Models.UserModel> tempData = DoctorProcessor.LoadPatientInfo(currentPatient.Id);
            tempPatientData = tempData.ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DataAccessPatientInfo));
            currentPatient = tempPatientData[0];      

            //----------------------    Getting Pateints Prescriptions
            List<DataLibrary.Models.PatientPrescriptions> data = PatientProcessor.LoadPatientPrescriptions(currentPatient.Id);
            List<PatientPrescriptions> patientPrescriptions = new List<PatientPrescriptions>();
            patientPrescriptions = data.ConvertAll(new Converter<DataLibrary.Models.PatientPrescriptions, PatientPrescriptions>(DataAccessPatientPerscriptions));
            currentPatient.SetPrescriptions(patientPrescriptions);

            return View(currentPatient);
        }

        //public ActionResult AddPrescription(AddPrescription model)
        //{
        //    patientId = model.Id;

        //    return View();
        //}

        public ActionResult AddPrescription()
        {           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPrescription(AddPrescription model)
        {
            if (ModelState.IsValid)
            {//the number 2 should be the doctor id not a 2// same with the 0 this might not need to change
                DoctorProcessor.AddPrescription(model.Id, 2, model.prescriptionFID, 0, model.name, model.dosage,
            model.pillCount, model.numberofRefills, model.useBefore, model.description, DateTime.Now.ToString());
                
                return RedirectToAction("Login", "Home", model);
            }
            return View();
        }


    }
}