using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int userID { get; set; }
        public string text { get; set; }
        public string user { get; set; }
        public string time { get; set; }
        public string date { get; set; }

    }
}