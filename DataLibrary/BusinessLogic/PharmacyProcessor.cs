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

        //FOR MERGE
        public static List<int> getAlreadyDelPers()
        {
            string sql = @"SELECT patientRXFID FROM dbo.[deliveries];";

            return SqlDataAccess.LoadData<int>(sql);
        }

        public static List<PatientPrescriptions> GetPrescriptionByPatient(int patientFID)
        {
            string sql = @"SELECT Id, patientFID, name, dosage, pillCount, 
                            numberofRefills, datePrescribed, description FROM dbo.[patientPrescriptions] WHERE patientFID = '" + patientFID + "';";

            return SqlDataAccess.LoadData<PatientPrescriptions>(sql);
        }

        public static List<int> getPatientIds()
        {
            string sql = @"SELECT Id FROM dbo.[user] WHERE accountType = 1;";

            return SqlDataAccess.LoadData<int>(sql);
        }

        public static int CreateDelivery(int PerId, int PatId, string shippedDate, string ArrivalDate)
        {
            Delivery data = new Delivery
            {
                patientFID = PerId,
                Id = PatId,
                shippedDate = shippedDate,
                arrivalDate = ArrivalDate                
            };
            string sql = @"INSERT into dbo.[deliveries] (patientFID, patientRXFID, shippedDate, arrivalDate)
                            values(@patientFID, @Id, @shippedDate, @arrivalDate)";

            return SqlDataAccess.SaveData(sql, data);
        }
        //
    }
}
