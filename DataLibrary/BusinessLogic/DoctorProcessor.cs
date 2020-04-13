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
    public class DoctorProcessor : Processor
    {
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

        public static List<int>GetPatientIds(int id)
        {
            string sql = @"SELECT patientFID FROM dbo.[bridgeDoctorPatient] WHERE doctorFID = '" + id + "';";

            return SqlDataAccess.LoadData<int>(sql);
        }

        public static List<UserModel> LoadPatientInfo(int id)
        {
            string sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode
                           FROM dbo.[user] WHERE Id = '"+ id +"';";          
            
            return SqlDataAccess.LoadData<UserModel>(sql);
        }
     
        public static int AddPrescription(int patientFID, int doctorFID, string name, string dosage,
            int pillCount, int numberofRefills, string useBefore, string description, string datePrescribed)
        {
            PatientPrescriptions data = new PatientPrescriptions
            {
                patientFID = patientFID,
                doctorFID = doctorFID,                                
                name = name,
                dosage = dosage,
                pillCount = pillCount,
                numberofRefills = numberofRefills,
                useBefore = useBefore,
                description = description,
                datePrescribed = datePrescribed
            };
            string sql = @"INSERT into dbo.[patientPrescriptions] (patientFID, doctorFID, prescriptionFID, name, dosage, pillCount, numberofRefills, useBefore, description, datePrescribed)
                            values(@patientFID, @doctorFID, @prescriptionFID, @name, @dosage, @pillCount, @numberofRefills, @useBefore, @description, @datePrescribed)";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int AddAppointment(int PatientFID, int DoctorFID, string date, string desc)
        {
            Appointment data = new Appointment
            {
                PatientFID = PatientFID,
                DoctorFID = DoctorFID,
                date = date,
                desc = desc
            };
            //string sql = @"INSERT into dbo.[appointments] (patientFID, doctorFID, date, `desc`, isconfirmed)
            //                values(@PatientFID, @DoctorFID, @date, `@desc`, '1')";

            string sql = @"INSERT into dbo.[appointments] (patientFID, doctorFID, date, [desc], isconfirmed)
                            values(@PatientFID, @DoctorFID, @date, '"+ desc + "', '1')";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<UserModel> LoadUser(int id)
        {
            string sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode, accountType, officeHours
                            FROM dbo.[user] WHERE Id = '" + id + "';";

            return SqlDataAccess.LoadData<UserModel>(sql);
        }

        public static int ChangeEmail(int Id, string email)
        {
            UserModel data = new UserModel
            {
                Id = Id,
                email = email
            };
            string sql = @"UPDATE dbo.[user] SET email = '" + email + "' WHERE Id = '" + Id + "';";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int ChangePhone(int Id, string phoneNumber)
        {
            UserModel data = new UserModel
            {
                Id = Id,
                phoneNumber = phoneNumber
            };
            string sql = @"UPDATE dbo.[user] SET phoneNumber = '" + phoneNumber + "' WHERE Id = '" + Id + "';";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int ChangePassword(int Id, string password)
        {
            UserModel data = new UserModel
            {
                Id = Id,
                password = password
            };
            string sql = @"UPDATE dbo.[user] SET passwword = '" + password + "' WHERE Id = '" + Id + "';";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int ChangeAddress(int Id, string streetAddress, string city, string state, string zipcode)
        {
            UserModel data = new UserModel
            {
                Id = Id,
                streetAddress = streetAddress,
                city = city,
                state = state,
                zipcode = zipcode
            };//NEED TO FINISH QUERY
            string sql = @"UPDATE dbo.[user] SET streetAddress = '"+ streetAddress +"', city = '"+ city +"', state = '"+ state +"', zipcode = '"+ zipcode +"'  WHERE Id = '" + Id + "';";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<Appointment> loadAppointmentData(int id)
        {
            string sql = @"SELECT Id, patientFID, doctorFID, date, [desc], isconfirmed FROM dbo.[appointments] WHERE doctorFID = '" + id + "';";
            List<Appointment> temp = SqlDataAccess.LoadData<Appointment>(sql);
            for (int i = 0; i < temp.Count(); i++)
            {
                sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode, accountType, officeHours
                            FROM dbo.[user] WHERE Id = '" + temp[i].DoctorFID + "';";
                List<UserModel> tempDoc = SqlDataAccess.LoadData<UserModel>(sql);
                temp[i].doctor = tempDoc[0];
            }
            return temp;
        }

        public static List<UserModel> checkIfPatientExists(string fName, string lName, string email, string streetAddress, string city, string state)
        {
            string sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode, accountType, officeHours
                            FROM dbo.[user] WHERE fName = '"+fName+"' AND lName = '"+lName+"' AND email = '"+email+"' AND streetAddress = '"+streetAddress+"' AND city = '"+city+"' AND state = '"+state+"';";

            return SqlDataAccess.LoadData<UserModel>(sql);
        }

        public static int AddPatient(int PatientFID, int DoctorFID)
        {
            Appointment data = new Appointment//this appointment doesnt apply its really for doctor patient bridge
            {
                PatientFID = PatientFID,
                DoctorFID = DoctorFID                
            };            

            string sql = @"INSERT into dbo.[bridgeDoctorPatient] (patientFID, doctorFID)
                            values(@PatientFID, @DoctorFID)";
            return SqlDataAccess.SaveData(sql, data);
        }
    }
}
