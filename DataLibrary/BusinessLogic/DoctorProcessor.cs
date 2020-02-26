using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using DataLibrary.Models;

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

        //going to be for certain doctor
        public static List<UserModel> LoadPatients()
        {
            string sql = @"SELECT Id, fName, lName, email, password, phoneNumber, streetAddress, city, state, zipcode
                            FROM dbo.[user];";

            return SqlDataAccess.LoadData<UserModel>(sql);
        }
    }
}
