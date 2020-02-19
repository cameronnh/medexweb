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
    public class HomeController : Controller
    {        
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult PatientRegister()
        {
            return View();
        }

        public ActionResult DoctorRegister()
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
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoctorRegister(DoctorRegister model)
        {
            if (ModelState.IsValid)
            {
                CreateDoctor(model.fName, model.lName, model.email, model.password,
                    model.phoneNumber, model.streetAddress, model.city,
                    model.state, model.zipcode, model.officeHours);
                return RedirectToAction("Login");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Login model)
        {

            var data = LoadUser(model.email, model.password);
            
            if (data.Count == 1)
            {
                UserModel user = new UserModel();

                foreach (var row in data)
                {
                    user.Id = row.Id;
                    user.fName = row.fName;
                    user.lName = row.lName;
                    user.email = row.email;
                    user.password = row.password;
                    user.phoneNumber = row.phoneNumber;
                    user.streetAddress = row.streetAddress;
                    user.city = row.city;
                    user.state = row.state;
                    user.zipcode = row.zipcode;
                    user.accountType = row.accountType;
                    user.officeHours = row.officeHours;
                }

                TempData["user"] = user;         
                Session["Id"] = user.Id;

                if(user.accountType == 1)
                {
                    return RedirectToAction("Index", "Patient");
                }
                else if(user.accountType == 2)
                {
                    return RedirectToAction("Index", "Doctor");
                }
                else if(user.accountType == 3)
                {
                    return View();
                }
                else if(user.accountType == 4)
                {
                    return View();//idk if we should do packer admin till really later
                }
                else
                {
                    return View();
                }

            }
            else
            {
                return View();
            }
            
        }

    }
}