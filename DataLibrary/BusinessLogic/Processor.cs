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
    }
}
