﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace medexnet.Models
{
    public class PharmacyPrescriptions
    {
        public int prescriptionFID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string prescriptionName { get; set; }
        public string dosage { get; set; }
        public int pillCount { get; set; }
        public string description { get; set; }
        public int numberofRefills { get; set; }
        public string useBefore { get; set; }
        public string datePrescribed { get; set; }
    }
}