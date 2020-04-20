using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLibrary.Models
{
    public class docPreClasses
    {
        public int classId { get; set; }
        public string className { get; set; }

        public List<docPre> prescriptions { get; set; }

        public docPreClasses()
        {
            prescriptions = new List<docPre>();            
        }
        //public List<PatientPrescriptions> GetPrescriptions() { return myPrescriptions; }


    }
}