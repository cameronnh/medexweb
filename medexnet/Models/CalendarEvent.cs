﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace medexnet.Models
{
    public class CalendarEvent
    {
        public int Sr { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string PriorityColor { get; set; }
    }
}