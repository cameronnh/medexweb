using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public class PatientProcessor : Processor
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

        public static List<PatientPrescriptions> LoadPatientPrescriptions(int id)
        {
            string sql = @"SELECT Id, patientFID, doctorFID, prescriptionFID, deliveryFID, name, dosage, pillCount, 
                            numberofRefills, useBefore, description, datePrescribed FROM dbo.[patientPrescriptions] WHERE patientFID = '" + id + "';";

            return SqlDataAccess.LoadData<PatientPrescriptions>(sql);
        }

        public static List<Delivery> loadPrescriptionDelivery(int id)
        {
            string sql = @"SELECT Id, shippedDate, arrivalDate FROM dbo.[deliveries] WHERE Id = '" + id + "';";

            return SqlDataAccess.LoadData<Delivery>(sql);
        }

        public static List<UserModel> loadDoctorData(int id)
        {
            string sql = @"SELECT doctorFID FROM dbo.[bridgeDoctorPatient] WHERE patientFID = '" + id + "';";
            List<int> temp = SqlDataAccess.LoadData<int>(sql);
            List<UserModel> DoctorList = new List<UserModel>();
            foreach(int I in temp)
            {
                sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode, accountType, officeHours
                            FROM dbo.[user] WHERE Id = '" + I + "';";
                List<UserModel> tempDoc = SqlDataAccess.LoadData<UserModel>(sql);
                DoctorList.Add(tempDoc[0]);
            }
            return DoctorList;
        }
        public static List<Appointment> loadAppointmentData(int id)
        {
            string sql = @"SELECT Id, patientFID, doctorFID, date, [desc], isconfirmed FROM dbo.[appointments] WHERE patientFID = '" + id + "';";
            List<Appointment> temp = SqlDataAccess.LoadData<Appointment>(sql);
            for(int i = 0; i < temp.Count(); i++)
            {
                 sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode, accountType, officeHours
                            FROM dbo.[user] WHERE Id = '" + temp[i].DoctorFID + "';";
                List<UserModel> tempDoc = SqlDataAccess.LoadData<UserModel>(sql);
                temp[i].doctor = tempDoc[0];
            }
            return temp;
        }
    }
}
