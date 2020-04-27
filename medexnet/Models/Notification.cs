using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace medexnet.Models
{
    public class Notification
    {
        public enum NotificationType { message, appointment, prescription, delivery}
        public int id { get; set; }
        public string description { get; set; }
        public NotificationType type { get; set; }
        public string time { get; set; }
        public string image { get; set; }
        public string address { get; set; }


        public Notification()
        {
            image = "ti-calendar";
        }
    }
}