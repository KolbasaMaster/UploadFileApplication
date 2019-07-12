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
        static IModel model;
        readonly IConnection connection;
        private static string queueName = "Queue";
        
        public ReceiveMessagesFromRabbit()
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            model = connection.CreateModel();
            model.QueueDeclare(queueName, false, false, false, null);
        }

        //public void ReceiveRabbit(ConcurrentQueue<string> queue)

        //{
        //    try
        //    {
        //        var consumer = new EventingBasicConsumer(model);
        //        consumer.Received += (mod, ea) =>
        //        {
        //            var body = ea.Body;
        //            var message = Encoding.UTF8.GetString(body);
        //            queue.Enqueue(message);
        //        };
        //        model.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);       
        //    }
        //    catch // прописывать тип исключения 
        //    {
        //    }
        //}
        public void ReceiveRabbit(Queue<string> messages)
        {
            try
            {
                var consumer = new EventingBasicConsumer(model);
                consumer.Received += (mod, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    lock (message)
                    {
                        messages.Enqueue(message);
                    }
                };
                model.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            }
            catch
            {
            }
        }
    }
}
