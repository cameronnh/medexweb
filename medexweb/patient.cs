using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace medexweb
{
    public class patient : user
    {
        public int ID = -1;
        public List<prescription> myPrescriptions = new List<prescription>();
        public string eMailAddress;
        public string phoneNumber;
        public string address;
        public string DoB;

        public patient() { }

        public patient(string patientName)
        {
            myName = patientName;
        }

        public patient(int inID, string tempName)
        {
            ID = inID;
            myName = tempName;

        }
        public patient(int id, string patientName, string patientUserName, string patientPassword, string patientAddress,
            string patientEMail, string patientPhoneNumber)
        {
            ID = id;
            myName = patientName;
            userName = patientUserName;
            password = patientPassword;
            eMailAddress = patientEMail;
            phoneNumber = patientPhoneNumber;
            address = patientAddress;

        }
        public void SetPrescriptions(prescription tempPrescription)
        {
            myPrescriptions.Add(tempPrescription);
        }

        public void setDob(string tempString)
        {
            DoB = tempString;
        }
    }
}
