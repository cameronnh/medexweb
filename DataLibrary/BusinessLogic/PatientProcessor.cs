using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class PatientProcessor
    {
        //this is patient register
        public static int CreatePatient(string fName, string lName, string email,
            string password, string phoneNumber, string streetAddress, string city,
            string state, string zipcode)
        {
            PatientModel data = new PatientModel
            {               
                fName = fName,
                lName = lName,
                email = email,
                password = password,
                phoneNumber = phoneNumber,
                streetAddress = streetAddress,
                city = city,
                state = state,
                zipcode = zipcode
            };
            string sql = @"INSERT into dbo.[user] (fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode)
                            values(@fName, @lName, @email, @password, @phoneNumber, @streetAddress, @city, @state, @zipcode)";

            return SqlDataAccess.SaveData(sql, data);
        }

        //goigg to be for cerain doctor
        public static List<PatientModel> LoadPatients()
        {
            string sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode
                            FROM dbo.[user];";

            return SqlDataAccess.LoadData<PatientModel>(sql);
        }

        //this is used for login and it passes a patient model to 
        public static List<PatientModel> LoadPatient(string email, string password)
        {
            string sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode
                            FROM dbo.[user] WHERE email = '" + email + " ' AND password = '" + password + "';";

            return SqlDataAccess.LoadData<PatientModel>(sql);
        }

        public static List<PatientPrescriptions> LoadPatientPrescriptions(int id)
        {
            string sql = @"SELECT Id, patientFID, doctorFID, prescriptionFID, deliveryFID, name, dosage, pillCount, 
                            numberofRefills, useBefore, description, datePrescribed FROM dbo.[patientPresciption] WHERE patientFID = '" + id + "';";

            return SqlDataAccess.LoadData<PatientPrescriptions>(sql);
        }
    }
}
