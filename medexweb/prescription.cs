using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace medexweb
{
    public class prescription
    {
        public DateTime myDeliveryDate { get; set; }
        public int prescriptionId { get; set; }

        public string prescriptionName { get; set; }

        public string prescriptionDosage { get; set; }

        public int refillsize { get; set; }

        public int numberofdays { get; set; }

        public string prescriptionDesc { get; set; }
        public string classification;
        public int classificationID;


        public prescription(int RxID, string RxName, string RxDosage, int RxNumberofPills, int RxSupplyDays, string rxDesc)
        {
            myDeliveryDate = new DateTime();
            prescriptionId = RxID;
            prescriptionName = RxName;
            prescriptionDosage = RxDosage;
            refillsize = RxNumberofPills;
            numberofdays = RxSupplyDays;
            prescriptionDesc = rxDesc;
        }

        public prescription(int RxID, string RxName, string RxDosage, string RxClass, int RxClassID)
        {
            myDeliveryDate = new DateTime();
            prescriptionId = RxID;
            prescriptionName = RxName;
            prescriptionDosage = RxDosage;
            classification = RxClass;
            classificationID = RxClassID;
        }
    }
}
