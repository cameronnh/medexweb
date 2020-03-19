using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using medexnet.Models;

namespace medexnet.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public int accountType { get; set; }
        public string officeHours { get; set; }
        public List<PatientPrescriptions> myPrescriptions { get; set; }
        public List<UserModel> myDoctors { get; set; }
        public List<UserModel> myPatients { get; set; }
        public List<Appointment> myAppointments { get; set; }

        public UserModel()
        {
            myPrescriptions = new List<PatientPrescriptions>();
            myDoctors = new List <UserModel>();
            myPatients = new List <UserModel>();
            myAppointments = new List<Appointment>();
        }
        public List<PatientPrescriptions> GetPrescriptions(){return myPrescriptions;}
        public void SetPrescriptions(List<PatientPrescriptions> patientPrescriptions) { myPrescriptions = patientPrescriptions; }
        public List<UserModel> GetPatients() { return myPatients;}
        public void SetPatients(List<UserModel> doctorsPatients) { myPatients = doctorsPatients; }
    }
}