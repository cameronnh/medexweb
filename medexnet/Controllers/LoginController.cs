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
            if (ModelState.IsValid)
            {
                //var data = ValidatePatient(model.email, model.password);                

                //if (data == null)
                //{
                //    return RedirectToAction("PatientLogin");                    
                //}
                //else
                //{
                //    PatientModel currentPatient = new PatientModel();

                //    currentPatient = data;

                //    foreach (var row in data)
                //    {
                //        currentPatient.Id = row.Id;
                //        currentPatient.fName = row.fName;
                //        currentPatient.lName = row.lName;
                //        currentPatient.email = row.email;
                //        currentPatient.password = row.password;
                //        currentPatient.phoneNumber = row.phoneNumber;
                //        currentPatient.streetAddress = row.streetAddress;
                //        currentPatient.city = row.city;
                //        currentPatient.state = row.state;
                //        currentPatient.zipcode = row.zipcode;

                //        currentPatient.Add(new PatientModel
                //        {
                //            Id = row.Id,
                //            fName = row.fName,
                //            lName = row.lName,
                //            email = row.email,
                //            password = row.password,
                //            phoneNumber = row.phoneNumber,
                //            streetAddress = row.streetAddress,
                //            city = row.city,
                //            state = row.state,
                //            zipcode = row.zipcode
                //        });
                //}

                Session["Id"] = "LOL"/*currentPatient.Id*/;
                return RedirectToAction("Index", "Home");
                //}
            }
            return View();
        }

    }
}