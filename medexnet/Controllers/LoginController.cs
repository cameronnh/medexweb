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
    public class LoginController : Controller
    {        
        public ActionResult PatientLogin()
        {
            return View();
        }

        public ActionResult PatientRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientRegister(PatientRegister model)
        {
            if (ModelState.IsValid)
            {
                CreatePatient(model.fName, model.lName, model.email, model.password,
                    model.phoneNumber, model.streetAddress, model.city,
                    model.state, model.zipcode);
                return RedirectToAction("PatientLogin");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PatientLogin(Patient_Login model)
        {

            var data = LoadPatient(model.email, model.password);
            
            if (data.Count == 1)
            {          
                PatientModel patient = new PatientModel();

                foreach (var row in data)
                {
                    patient.Id = row.Id;
                    patient.fName = row.fName;
                    patient.lName = row.lName;
                    patient.email = row.email;
                    patient.password = row.password;
                    patient.phoneNumber = row.phoneNumber;
                    patient.streetAddress = row.streetAddress;
                    patient.city = row.city;
                    patient.state = row.state;
                    patient.zipcode = row.zipcode;
                }

                TempData["patient"] = patient;                
                Session["Id"] = patient.Id;
                return RedirectToAction("Index", "Home");             
            }
            else
            {

                return View();
            }
            
        }

    }
}