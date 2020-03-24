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
    public class PharmacyController : Controller
    {
        public static UserModel currentPacker = new UserModel();
        public static List<PatientPrescriptions> allPrescriptions = new List<PatientPrescriptions>();
        public ActionResult Index(UserModel packer)
        {
            currentPacker = packer;
        
            return View(currentPacker);
        }

        public ActionResult Prescriptions()
        {
            List<PatientPrescriptions> tempPrescriptions = new List<PatientPrescriptions>();
            List<DataLibrary.Models.PatientPrescriptions> data = PharmacyProcessor.GetAllPrescription();
            tempPrescriptions = data.ConvertAll(new Converter<DataLibrary.Models.PatientPrescriptions, PatientPrescriptions>(DataAccessPatientPerscriptions));
            allPrescriptions = tempPrescriptions;

            return View(allPrescriptions);
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