﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushNotification.Model
{
    public class EmailConfigDTO
    {
        public string ConnectionName { get; set; }
        public string Description { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public string status { get; set; }

    }
}
