using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using DataLibrary.Models;
using System.Data.SqlClient;

namespace DataLibrary.BusinessLogic
{
    public class PharmacyProcessor
    {
        public static int CreatePharmacist(string fName, string lName, string email,
            string password, string phoneNumber)
        {
            UserModel data = new UserModel
            {
                fName = fName,
                lName = lName,
                email = email,
                password = password,
                phoneNumber = phoneNumber,
                accountType = 3
            };
            string sql = @"INSERT into dbo.[user] (fName, lName, email, password, phoneNumber, accountType)
                            values(@fName, @lName, @email, @password, @phoneNumber, @accountType)";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<PatientPrescriptions> GetAllPrescription()
        {
            string sql = @"SELECT Id, patientFID, doctorFID, prescriptionFID, name, dosage, pillCount, 
                            numberofRefills, useBefore, description, datePrescribed FROM dbo.[patientPrescriptions];" ;

            return SqlDataAccess.LoadData<PatientPrescriptions>(sql);
        }

    }
}
