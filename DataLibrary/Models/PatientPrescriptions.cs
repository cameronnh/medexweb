using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class PatientPrescriptions
    {
        public int Id { get; set; }
        public int patientFID { get; set; }
        public int doctorFID { get; set; }
        public int prescriptionFID { get; set; }
        public int deliveryFID { get; set; }
        public string name { get; set; }
        public string dosage { get; set; }
        public int pillCount { get; set; }
        public int numberofRefills { get; set; }
        public string useBefore { get; set; }
        public string description { get; set; }
        public string datePrescribed { get; set; }
    }
}
