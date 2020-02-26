using medexnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using DataLibrary.BusinessLogic;

namespace medexnet.Controllers
{
    public class PatientController : Controller
    {
        public ActionResult Index(UserModel patient)
        {  
            List<DataLibrary.Models.PatientPrescriptions> data = PatientProcessor.LoadPatientPrescriptions(patient.Id);
            List<PatientPrescriptions> patientPrescriptions = new List<PatientPrescriptions>();
            patientPrescriptions = data.ConvertAll(new Converter<DataLibrary.Models.PatientPrescriptions, PatientPrescriptions>(DALToMedex));
            patient.SetPrescriptions(patientPrescriptions);

            return View(patient);
        }
        public static PatientPrescriptions DALToMedex(DataLibrary.Models.PatientPrescriptions temp)
        {
            return new PatientPrescriptions
                {
                    id = temp.id,
                    patientId = temp.patientId,
                    doctorId = temp.doctorId,
                    prescriptionId = temp.prescriptionId,
                    deliveryId = temp.deliveryId,
                    name = temp.name,
                    dosage = temp.dosage,
                    pillcount = temp.pillcount,
                    numberofrefills = temp.numberofrefills,
                    useBefore = temp.useBefore,
                    description = temp.description,
                    datePrescribed = temp.datePrescribed
                 };
        }

        public ActionResult Perscriptions(UserModel patient)
        {
            ViewBag.Message = "Your application description page.";

            return View(patient);
        }

        public ActionResult Deliveries(UserModel patient)
        {

            return View(patient);
        }
        public ActionResult DoctorInfo(UserModel patient)
        {

            return View(patient);
        }
        public ActionResult Appointments(UserModel patient)
        {

            return View(patient);
        }
        public ActionResult MessageInbox(UserModel patient)
        {

            return View(patient);
        }
        public ActionResult Settings(UserModel patient)
        {

            return View(patient);
        }


    }
}