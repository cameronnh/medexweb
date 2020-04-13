using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace medexnet.Models
{
    public class DoctorEvents
    {
        public string Date { get; set; }
        public string Patient_Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Patient_Phonenumber { get; set; }
    }
}