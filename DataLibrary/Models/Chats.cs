using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Chats
    {
        public int Id { get; set; }
        public string topic { get; set; }
        public int patientID { get; set; }
        public int doctorID { get; set; }
        public List<Message> messageList { get; set; }
        public Chats()
        {
            messageList = new List<Message>();
        }
    }
}