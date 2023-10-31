using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PushNotification.Model;

namespace PushNotification.Interface
{
    public interface IServiceSchedular
    {
        void CreateSchedular(SchedularConfig schedularDTO);
    }
}
