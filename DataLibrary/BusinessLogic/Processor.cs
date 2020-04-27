using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public class Processor
    {
        //this is used for login and it passes a patient model to 
        public static List<UserModel> LoadUser(string email, string password)
        {
            string sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode, accountType, officeHours
                            FROM dbo.[user] WHERE email = '" + email + " ' AND password = '" + password + "';";

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
            string sql = @"UPDATE dbo.[user] SET streetAddress = '" + streetAddress + "', city = '" + city + "', state = '" + state + "', zipcode = '" + zipcode + "'  WHERE Id = '" + Id + "';";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int AddChat(int patientID, int doctorID, string topic)
        {
            Chats data = new Chats
            {
                patientID = patientID,
                doctorID = doctorID,
                topic = topic
            };
            string sql = @"INSERT into dbo.[Chats] (patientID, doctorID, topic)
                            values(@patientID, @doctorID, @topic)";

            return SqlDataAccess.SaveData(sql, data);
        }
        public static int AddMessage(int userId, string text, string user, string time, string date, int chatID)
        {
            Message data = new Message
            {
                userID = userId,
                text = text,
                user = user,
                time = time,
                date = date
            };
            string sql = @"INSERT into dbo.[messages] (userId, text, [user], time, date, chatID)
                            values(@userId, @text, @user, @time, @date, " + chatID + ")";

            return SqlDataAccess.SaveData(sql, data);
        }
        
    }
}
