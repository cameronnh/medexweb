﻿using medexnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using DataLibrary.BusinessLogic;

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

        public ActionResult PharmacistRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientRegister(PatientRegister model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PatientProcessor.CreatePatient(model.fName, model.lName, model.email, model.password,
                        model.phoneNumber, model.streetAddress, model.city,
                        model.state, model.zipcode);
                    return RedirectToAction("Login", "Home", model);
                }
                catch
                {
                    ModelState.AddModelError("email", "There is already a user with this email. Please use a different email.");
                    return View(model);
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoctorRegister(DoctorRegister model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DoctorProcessor.CreateDoctor(model.fName, model.lName, model.email, model.password,
                        model.phoneNumber, model.streetAddress, model.city,
                        model.state, model.zipcode, model.officeHours);
                    return RedirectToAction("Login", "Home", model);
                }
                catch
                {
                    ModelState.AddModelError("email", "There is already a user with this email. Please use a different email.");
                    return View(model);
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PharmacistRegister(PharmacistRegister model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PharmacyProcessor.CreatePharmacist(model.fName, model.lName, model.email, model.password,
                        model.phoneNumber);
                    return RedirectToAction("Login", "Home", model);
                }
                catch
                {
                    ModelState.AddModelError("email", "There is already a user with this email. Please use a different email.");
                    return View(model);
                }
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Login model)
        {
            List<DataLibrary.Models.UserModel> data = Processor.LoadUser(model.email, model.password);
            
            if (data.Count > 0)
            {
                List<UserModel> userList = new List<UserModel>();

                userList = data.ConvertAll(new Converter<DataLibrary.Models.UserModel, UserModel>(DALtoMedex.GetUserData));
                UserModel user = userList[0];

                Session["Id"] = user.Id;

                if(user.accountType == 1)
                {
                    return RedirectToAction("Index", "Patient", user);
                }
                else if(user.accountType == 2)
                {
                    return RedirectToAction("Index", "Doctor", user);
                }
                else if(user.accountType == 3)
                {
                    return RedirectToAction("Index", "Pharmacy", user);
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

        public ActionResult LogOut()
        {           
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
    }
}