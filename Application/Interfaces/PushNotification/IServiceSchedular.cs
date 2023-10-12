﻿using Application.DTOs.PushNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.PushNotification
{
    public interface IServiceSchedular
    {
        public Task<ServiceSchedularList> GetServiceschedularList(ServiceSchedularDTO serviceSchedularDTO);
        public Task<ServiceSchedularDTO> ServiceSchedularStatusUpdate(ServiceSchedularDTO serviceSchedularDTO);

    }
}