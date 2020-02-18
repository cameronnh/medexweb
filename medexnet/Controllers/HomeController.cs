using medexnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using static DataLibrary.BusinessLogic.PatientProcessor;

namespace medexnet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {                      
            PatientModel patient = (PatientModel)TempData["patient"];
            TempData.Keep("patient");

            //ViewBag.PatientFirstName = patient.fName;
            
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