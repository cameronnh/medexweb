using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace medexnet.Models
{
    public class AddPrescription
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must have a prescription.")]
        public int prescriptionFID { get; set; }     

        [Required(ErrorMessage = "You must have a name.")]
        public string name { get; set; }

        [Required(ErrorMessage = "You must have a dosage.")]
        public string dosage { get; set; }

        [Required(ErrorMessage = "You must have a pill count.")]
        public int pillCount { get; set; }

        [Required(ErrorMessage = "You must have a number of refills.")]
        public int numberofRefills { get; set; }

        [Required(ErrorMessage = "You must have a use before date.")]
        [DataType(DataType.Date)]
        public string useBefore { get; set; }

        [Required(ErrorMessage = "You must have a description.")]
        public string description { get; set; }
       
    }
}