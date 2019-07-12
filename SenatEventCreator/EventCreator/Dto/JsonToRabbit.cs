using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using RabbitMQ.Client.Events;
using System.Threading;

namespace EventCreator.Dto
{
    public class JsonToRabbit : IDisposable
    {
        string exchangeName = "Exchange";
        string queueName = "Queue";
        string routingKey = "Key";
        readonly IModel model;
        readonly IConnection connection;

        public JsonToRabbit()
        {
            ConnectionFactory factory = new ConnectionFactory();
            connection = factory.CreateConnection();
            model = connection.CreateModel();
            model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            model.QueueDeclare(queueName, false, false, false, null);
            model.QueueBind(queueName, exchangeName, routingKey, null);
        }

        public void AddToRabbit(string json)
        {
            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(json);
            model.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);
        }
        
        public void Dispose()
        {
            model?.Close();
            connection?.Close();
            model?.Dispose();
            connection?.Dispose();
        }
    }
}
