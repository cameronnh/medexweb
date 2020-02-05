using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class PatientProcessor
    {
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

            string sql = @"INSERT into dbo.Patient (fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode)
                            values(@fName, @lName, @email, @password, @phoneNumber, @streetAddress, @city, @state, @zipcode)";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<PatientModel> LoadPatients()
        {
            string sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode
                            FROM dbo.Patient;";

            return SqlDataAccess.LoadData<PatientModel>(sql);
        }

        public static List<PatientModel> ValidatePatient(string email, string password)
        {
            PatientModel data = new PatientModel
            {
                email = email,
                password = password
            };

            string sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode
                            FROM dbo.Patient WHERE email = @email AND @password;";

            return SqlDataAccess.LoadData<PatientModel>(sql);
        }
        
        
    }
}
