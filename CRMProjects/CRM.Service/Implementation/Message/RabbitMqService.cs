using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Service.Interface.Message;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;

namespace CRM.Service.Implementation.Message
{

    public class RabbitMqService : IRabbitMqService
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqService(IConfiguration config)
        {
            _factory = new ConnectionFactory
            {
                HostName = config["RabbitMq:HostName"] ?? "localhost"
            };
        }

        public async Task Publish(string queueName, string message)
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queueName, durable: false, exclusive: false, autoDelete: false);

            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync("", queueName, body);
        }
    }


}
