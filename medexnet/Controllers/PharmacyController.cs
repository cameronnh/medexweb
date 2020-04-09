using medexnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using DataLibrary.BusinessLogic;
using System.Drawing;
//using DataLibrary.Models.DataLibrary.Models.PharmacyPrescriptions;

namespace medexnet.Controllers
{    
    public class PharmacyController : Controller
    {
        public static UserModel currentPacker = new UserModel();
        public static List<PharmacyPrescriptions> allPrescriptions = new List<PharmacyPrescriptions>();
        public ActionResult Index(UserModel packer)
        {
            currentPacker = packer;
        
            return View(currentPacker);
        }

        public ActionResult Prescriptions()
        {
            List<PharmacyPrescriptions> tempPrescriptions = new List<PharmacyPrescriptions>();
            List<DataLibrary.Models.PharmacyPrescriptions> data = PharmacyProcessor.GetPrescriptionsByPatient();
            tempPrescriptions = data.ConvertAll(new Converter<DataLibrary.Models.PharmacyPrescriptions, PharmacyPrescriptions>(DataAccessPharmacyPrescriptions));
            allPrescriptions = tempPrescriptions;

            return View(allPrescriptions);
        }

        public static medexnet.Models.PharmacyPrescriptions DataAccessPharmacyPrescriptions(DataLibrary.Models.PharmacyPrescriptions temp)
        {
            return new medexnet.Models.PharmacyPrescriptions
            {
                prescriptionFID = temp.prescriptionFID,
                fName = temp.fName,
                lName = temp.lName,
                prescriptionName = temp.name,
                dosage = temp.dosage,
                pillCount = temp.pillCount,
                numberofRefills = temp.numberofRefills,
                useBefore = temp.useBefore,
                description = temp.description,
                datePrescribed = temp.datePrescribed
            };
        }
    }
}