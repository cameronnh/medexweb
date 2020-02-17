using medexnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;

namespace medexnet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {                      
            PatientModel patient = (PatientModel)TempData["patient"];
            TempData.Keep("patient");

            ViewBag.PatientFirstName = patient.fName;          
            
            return View(patient);
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