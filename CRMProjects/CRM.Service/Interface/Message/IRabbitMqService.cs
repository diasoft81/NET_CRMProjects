﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Service.Interface.Message
{
    public interface IRabbitMqService
    {
        Task Publish(string queueName, string message);
    }

}
