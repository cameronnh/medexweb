using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientFID { get; set; }
        public int DoctorFID { get; set; }
        public string date { get; set; }
        public string desc { get; set; }
        public bool isConfirmed { get; set; }
        public UserModel doctor { get; set; }
        public UserModel patient { get; set; }

    }
}
