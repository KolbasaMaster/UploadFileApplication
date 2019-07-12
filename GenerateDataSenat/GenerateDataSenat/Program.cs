using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GenerateDataSenat.Streams;
using System.Collections.Concurrent;





namespace GenerateDataSenat
{
    public class Program
    {
        public static Queue<string> messages = new Queue<string>();
      //  public static ValidatorJson validator = new ValidatorJson();
        static ConcurrentQueue<string> Cb = new ConcurrentQueue<string>();
        static ReceiveMessagesFromRabbit rabbit = new ReceiveMessagesFromRabbit();

        //        public static void Main(string[] args)
        //        {


        //            Thread thread1 = new Thread(() => ReceiveMessagesFromDir.ReceiveDir(Cb));
        //            thread1.Start();
        //            Thread thread = new Thread(() => ReceiveMessagesFromRabbit.ReceiveRabbit(Cb));
        //            thread.Start();

        //            Thread thread2 = new Thread(() => ValidatorJson.Validator(Cb));
        //            thread2.Start();
        //if (Console.ReadKey().Key == ConsoleKey.Enter)
        //{
        //    Environment.Exit(0);
        //}
        //        }
        //    }
        //}
        public static void Main(string[] args)
        {
            Thread thread1 = new Thread(() => ReceiveMessagesFromDir.ReceiveDir(messages));
            thread1.Start();
            Thread thread = new Thread(() =>  rabbit.ReceiveRabbit(messages));
            thread.Start();
            Thread thread2 = new Thread(() => ValidatorJson.ValidateAndSend(messages));
            thread2.Start();
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Environment.Exit(0);
            }

        }
    }
}
