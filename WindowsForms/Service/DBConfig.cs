﻿using PushNotification.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PushNotification.Model;
using PushNotification.Interface;
namespace PushNotification.Service
{
    public class DBConfig
    {
        public List<DBConfigConnection> DBConfigs = new List<DBConfigConnection>();
    }
}