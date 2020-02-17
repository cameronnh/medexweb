using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class PatientPrescriptions
    {
        public int id { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public int prescriptionId { get; set; }
        public int deliveryId { get; set; }
        public string name { get; set; }
        public string dosage { get; set; }
        public int pillcount { get; set; }
        public int numberofrefills { get; set; }
        public string useBefore { get; set; }
        public string description { get; set; }
        public string datePrescribed { get; set; }
    }
}
