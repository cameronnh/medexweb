using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string shippedDate { get; set; }
        public string arrivalDate { get; set; }
        public string prescriptionName { get; set; }
        public string prescriptionDosage { get; set; }
        public int patientFID { get; set; }
    }
}
