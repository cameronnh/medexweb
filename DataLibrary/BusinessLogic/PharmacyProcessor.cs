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

        public static List<PharmacyPrescriptions> GetPrescriptionsByPatient()
        {
            string sql = @"SELECT [MedDB].[dbo].[patientPrescriptions].prescriptionFID, [MedDB].[dbo].[user].[fName], [MedDB].[dbo].[user].lName,
                            [MedDB].[dbo].[patientPrescriptions].[name], patientPrescriptions.dosage, patientPrescriptions.pillCount,
                            [patientPrescriptions].[description], patientPrescriptions.numberofRefills, patientPrescriptions.useBefore,
                            patientPrescriptions.datePrescribed
                           FROM [MedDB].[dbo].[patientPrescriptions]
                           INNER JOIN [MedDB].[dbo].[user] ON [MedDB].[dbo].[patientPrescriptions].patientFID=[MedDB].[dbo].[user].Id
                           ORDER BY [MedDB].[dbo].[user].lName;";

            return SqlDataAccess.LoadData<PharmacyPrescriptions>(sql);
        }
    }
}
