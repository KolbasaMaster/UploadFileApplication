using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GenerateDataSenat.Streams
{
    public class ReceiveMessagesFromRabbit
    {
        readonly IModel model;
        readonly IConnection connection;
        private string queueName = "Queue";
        
        public ReceiveMessagesFromRabbit()
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            model = connection.CreateModel();
            model.QueueDeclare(queueName, false, false, false, null);
        }

        //public void ReceiveRabbit(ConcurrentQueue<string> Cq)

        //{
        //    try
        //    {
        //        var consumer = new EventingBasicConsumer(model);
        //        consumer.Received += (mod, ea) =>
        //        {
        //            var body = ea.Body;
        //            var message = Encoding.UTF8.GetString(body);
        //            Task.Run(() => Cq.Enqueue(message));
        //        };
        //        model.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        //        Thread.Sleep(2000);
        //    }
        //    catch
        //    {
        //    }
        //}
        public void ReceiveRabbit(Queue<string> messages)
        {
            try
            {
                Monitor.Enter(messages);
                var consumer = new EventingBasicConsumer(model);
                consumer.Received += (mod, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    messages.Enqueue(message);

                };
                model.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            }
            finally
            {
                Monitor.Exit(messages);
            }
        }
    }
}
