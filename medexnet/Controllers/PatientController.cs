using medexnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using static DataLibrary.BusinessLogic.Processor;

namespace medexnet.Controllers
{
    public class PatientController : Controller
    {   
        public ActionResult Index()
        {
            UserModel patient = (UserModel)TempData["user"];
            TempData.Keep("user");

            //can be moved    
            var data = LoadPatientPrescriptions(patient.Id);
            List<PatientPrescriptions> patientPrescriptions = new List<PatientPrescriptions>();

            foreach (var row in data)
            {
                patientPrescriptions.Add(new PatientPrescriptions
                {
                    id = row.id,
                    patientId = row.patientId,
                    doctorId = row.doctorId,
                    prescriptionId = row.prescriptionId,
                    deliveryId = row.deliveryId,
                    name = row.name,
                    dosage = row.dosage,
                    pillcount = row.pillcount,
                    numberofrefills = row.numberofrefills,
                    useBefore = row.useBefore,
                    description = row.description,
                    datePrescribed = row.datePrescribed
                });
            }
            TempData["prescriptions"] = patientPrescriptions;
            //

            return View(patientPrescriptions);

        }

        public ActionResult Perscriptions()
        {
            List<PatientPrescriptions> prescriptions = (List<PatientPrescriptions>)TempData["prescriptions"];
            TempData.Keep("prescriptions");

            ViewBag.Message = "Your application description page.";

            return View(prescriptions);
        }
        
       
    }
}