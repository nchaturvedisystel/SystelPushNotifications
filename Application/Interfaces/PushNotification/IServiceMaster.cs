﻿using Application.DTOs.PushNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.PushNotification
{
    public interface IServiceMaster
    {
        public ServiceMasterList GetServiceMasterList();
    }
}