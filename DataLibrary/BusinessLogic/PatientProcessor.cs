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
    public static class Processor
    {
        //this is patient register
        public static int CreatePatient(string fName, string lName, string email,
            string password, string phoneNumber, string streetAddress, string city,
            string state, string zipcode)
        {
            UserModel data = new UserModel
            {               
                fName = fName,
                lName = lName,
                email = email,
                password = password,
                phoneNumber = phoneNumber,
                streetAddress = streetAddress,
                city = city,
                state = state,
                zipcode = zipcode,
                accountType = 1                
            };
            string sql = @"INSERT into dbo.[user] (fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode, accountType)
                            values(@fName, @lName, @email, @password, @phoneNumber, @streetAddress, @city, @state, @zipcode, @accountType)";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int CreateDoctor(string fName, string lName, string email,
            string password, string phoneNumber, string streetAddress, string city,
            string state, string zipcode, string officeHours)
        {
            UserModel data = new UserModel
            {
                fName = fName,
                lName = lName,
                email = email,
                password = password,
                phoneNumber = phoneNumber,
                streetAddress = streetAddress,
                city = city,
                state = state,
                zipcode = zipcode,
                officeHours = officeHours,
                accountType = 2
            };
            string sql = @"INSERT into dbo.[user] (fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode, accountType, officeHours)
                            values(@fName, @lName, @email, @password, @phoneNumber, @streetAddress, @city, @state, @zipcode, @accountType, @officeHours)";

            return SqlDataAccess.SaveData(sql, data);
        }

        //goigg to be for cerain doctor
        public static List<UserModel> LoadPatients()
        {
            string sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode
                            FROM dbo.[user];";

            return SqlDataAccess.LoadData<UserModel>(sql);
        }

        //this is used for login and it passes a patient model to 
        public static List<UserModel> LoadUser(string email, string password)
        {
            string sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode, accountType, officeHours
                            FROM dbo.[user] WHERE email = '" + email + " ' AND password = '" + password + "';";

            return SqlDataAccess.LoadData<UserModel>(sql);
        }

        public static List<PatientPrescriptions> LoadPatientPrescriptions(int id)
        {
            string sql = @"SELECT Id, patientFID, doctorFID, prescriptionFID, deliveryFID, name, dosage, pillCount, 
                            numberofRefills, useBefore, description, datePrescribed FROM dbo.[patientPrescriptions] WHERE patientFID = '" + id + "';";

            return SqlDataAccess.LoadData<PatientPrescriptions>(sql);
        }
    }
}
