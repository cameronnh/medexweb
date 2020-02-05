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
            if(ModelState.IsValid)
            {
                var data = ValidatePatient(model.email, model.password);

                if (data == null)
                {
                    PatientModel currentPatient = new PatientModel();

                    //currentPatient = data;

                    //foreach (var row in data)
                    //{
                    //    currentPatient.Add(new PatientModel
                    //    {
                    //        Id = row.Id,
                    //        fName = row.fName,
                    //        lName = row.lName,
                    //        email = row.email,
                    //        password = row.password,
                    //        phoneNumber = row.phoneNumber,
                    //        streetAddress = row.streetAddress,
                    //        city = row.city,
                    //        state = row.state,
                    //        zipcode = row.zipcode
                    //    });
                    //}

                    Session["Id"] = currentPatient.Id;
                    return View();
                }
                else
                {
                    return RedirectToAction("PatientLogin");
                }

                
            }

            return View();
        }

    }
}