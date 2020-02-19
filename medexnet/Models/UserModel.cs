using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace medexnet.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public int accountType { get; set; }
        public string officeHours { get; set; }
    }
}