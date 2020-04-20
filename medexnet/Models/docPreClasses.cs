using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace medexnet.Models
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