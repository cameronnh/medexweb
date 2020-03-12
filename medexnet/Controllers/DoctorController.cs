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
                id = temp.id,
                patientId = temp.patientId,
                doctorId = temp.doctorFID,
                prescriptionId = temp.prescriptionFID,
                deliveryId = temp.deliveryFID,
                name = temp.name,
                dosage = temp.dosage,
                pillcount = temp.pillCount,
                numberofrefills = temp.numberofRefills,
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

        public ActionResult PatientModal(UserModel currentPatient)
        {


            return View(currentPatient);
        }

        
    }
}