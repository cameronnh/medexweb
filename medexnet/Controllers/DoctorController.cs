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
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index(UserModel user)
        {
            //UserModel doctor = (UserModel)TempData["user"];
            //TempData.Keep("user");           

            //var data = LoadUser(doctor.Id);            
            //List<UserModel> users = new List<UserModel>();

            //foreach (var row in data)
            //{
            //    users.Add(new UserModel
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
            //        zipcode = row.zipcode,
            //        accountType = row.accountType,
            //        officeHours = row.officeHours
            //    });
                
            //}
            //TempData["patients"] = users;

            return View(user);
        }

        public ActionResult Patients()
        {



            return View();
        }

        
    }
}