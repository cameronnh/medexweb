using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLibrary.Models
{
    public class docPre
    {
        public int prescriptionId { get; set; }
        public string prescriptionName { get; set; }
        public string prescriptionDosage { get; set; }

        public int classFID { get; set; }
    }
}