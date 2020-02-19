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
                   
            var data = LoadPatientPrescriptions(patient.Id);
            List<PatientPrescriptions> patientPrescriptions = new List<PatientPrescriptions>();

            foreach (var row in data)
            {
                patientPrescriptions.Add(new PatientPrescriptions
                {
                    name = row.name,
                    dosage = row.dosage
                });
            }
            
            return View(patientPrescriptions);//might have to put patient in here aswell
            //Could just allow patients to have a list of Prescriptions in their object definition.-Connor

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
    }
}