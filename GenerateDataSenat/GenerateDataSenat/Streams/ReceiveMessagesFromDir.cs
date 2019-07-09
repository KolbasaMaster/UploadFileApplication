using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace GenerateDataSenat.Streams
{

    public class ReceiveMessagesFromDir
    {
        FileSystemWatcher watcher =
            new FileSystemWatcher(@"C:\Users\OUT-Reutov-VA\source\repos\SenatEventCreator\EventCreator\bin\Debug\json");

        //        public void ReceiveDir(ConcurrentQueue<string> cq)
        //        {
        //            try
        //            {               
        //                    while (true)
        //                    {
        //                        string path =
        //                            @"C:\Users\OUT-Reutov-VA\source\repos\SenatEventCreator\EventCreator\bin\Debug\json";
        //                        string[] dirs = Directory.GetFiles(path);
        //                        foreach (var file in dirs)
        //                        {
        //                            var content = File.ReadAllText(file);
        //                            Task.Run(() => cq.Enqueue(content));
        //                            File.Delete(file);
        //                        }
        //                    }                
        //            }
        //            catch
        //            {

        //            }

        //        }
        //    }

        //}

        public void ReceiveDir(Queue<string> message)
        {
            watcher.Created += (sender, ea) =>
            {
                string path = @"C:\Users\OUT-Reutov-VA\source\repos\SenatEventCreator\EventCreator\bin\Debug\json";
                string[] dirs = Directory.GetFiles(path);

                foreach (var file in dirs)
                {
                    lock (message)
                    {
                        var content = File.ReadAllText(file);
                        message.Enqueue(content);
                        File.Delete(file);
                    }
                }
            };
            watcher.EnableRaisingEvents = true;

        }
    }
}


//        public void ReceiveDir(Queue<string> message)
//        {

//            while (true)
//            {
//                string path = @"C:\Users\OUT-Reutov-VA\source\repos\SenatEventCreator\EventCreator\bin\Debug\json";
//                string[] dirs = Directory.GetFiles(path);

//                foreach (var file in dirs)
//                {
//                    lock (message)
//                    {
//                        var content = File.ReadAllText(file);
//                        message.Enqueue(content);
//                        File.Delete(file);
//                    }
//                }
//            }
//        }
//    }
//}
    




