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
            string sql = @"SELECT Id, patientFID, doctorFID, prescriptionFID, name, dosage, pillCount, 
                            numberofRefills, useBefore, description, datePrescribed FROM dbo.[patientPrescriptions] WHERE patientFID = '" + id + "';";

            List<PatientPrescriptions> temp = SqlDataAccess.LoadData<PatientPrescriptions>(sql);
            foreach (PatientPrescriptions p in temp)
            {
                try
                {
                    sql = @"SELECT color FROM dbo.[prescriptioncolors] WHERE prescriptionFID = '" + p.Id + "' AND patientFID = '" + id + "';";
                    List<string> color = SqlDataAccess.LoadData<string>(sql);
                    p.color = color[0];
                }
                catch(Exception e)
                {
                    p.color = "#1bb9c2";
                }
            }
            return temp;
        }

        public static List<Delivery> loadPrescriptionDelivery(int id)
        {
            string sql = @"SELECT Id, shippedDate, arrivalDate FROM dbo.[deliveries] WHERE patientRXFID = '" + id + "';";

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

        public static int AddAppointment(int patientID, int doctorID, string date, string desc, bool isConfirmed)
        {
            Appointment data = new Appointment
            {
                PatientFID = patientID,
                DoctorFID = doctorID,
                date = date,
                desc = desc,
                isConfirmed = isConfirmed
            };
            string sql = @"INSERT into dbo.[appointments] (patientFID, doctorFID, date, [desc], isconfirmed)
                            values(@PatientFID, @DoctorFID, @date, @desc, @isConfirmed)";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int ChangeColor(int patientId, int rxId, string color)
        {
            PatientPrescriptions data = new PatientPrescriptions
            {
                patientFID = patientId,
                color = color,
                Id = rxId
            };
            int returnVal = 1;

            string sql = @"UPDATE dbo.[prescriptionColors] SET color = '" + color + "' WHERE prescriptionFID = '" + rxId + "' AND patientFID = '" + patientId + "';";
            returnVal = SqlDataAccess.SaveData(sql, data);

            if(returnVal == 0)
            {
                sql = @"INSERT into dbo.[prescriptionColors] (prescriptionFID, patientFID, color)
                            values(@Id, @patientFID, @color)";
                returnVal = SqlDataAccess.SaveData(sql, data);
            }
            return returnVal;
        }

        public static List<Chats> loadChats(int id)
        {
            string sql = @"SELECT Id, topic, doctorID, patientID FROM dbo.[chats] WHERE patientID = '" + id + "';";
            List<Chats> temp = SqlDataAccess.LoadData<Chats>(sql);
            foreach (Chats C in temp)
            {
                sql = @"SELECT Id, userId, text, user, time, date FROM dbo.[messages] WHERE chatID = '" + C.Id + "';";
                List<Message> tempMessage = SqlDataAccess.LoadData<Message>(sql);
                C.messageList = tempMessage;
            }
            return temp;
        }
    }
}
