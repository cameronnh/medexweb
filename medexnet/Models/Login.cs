﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace medexnet.Models
{
    public class Login
    {
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}