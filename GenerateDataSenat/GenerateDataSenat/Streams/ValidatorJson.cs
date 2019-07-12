using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GenerateDataSenat.Dto;
using GenerateDataSenat.Streams.SenatApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace GenerateDataSenat.Streams
{
    public class ValidatorJson
    {
        //public delegate Guid CreateUser(CreateEmployeeDto create);
        //public delegate Guid ChangeRole(CreateUserRoleDto change);
        //public delegate bool BlockUser(LockUser lockUser);
        //public static event CreateUser CreateUserEvent;
        //public static event ChangeRole ChangeRoleEvent;
        //public static event BlockUser BlockUserEvent;
        private static string messageJson;
        readonly static RestSenatApiClient_
            client = new RestSenatApiClient_("https://sandbox.senat.sbt-osop-224.sigma.sbrf.ru/SENP-3512");
        private static JsonSchemaGenerator generator = new JsonSchemaGenerator();
        readonly static JsonSchema schema = generator.Generate(typeof(CreateEmployeeDto));
        readonly static JsonSchema schema1 = generator.Generate(typeof(LockUser));
        readonly static JsonSchema schema2 = generator.Generate(typeof(CreateUserRoleDto));

        //        public void Validator(ConcurrentQueue<string> Cq)
        //        {
        //            while (true)
        //            {
        //                try
        //                {                   
        //                    string receiveMessage;
        //                    Cq.TryDequeue(out receiveMessage);
        //                    var json = receiveMessage;
        //                    var message = JObject.Parse(json);
        //                    var CreateJson = JsonConvert.DeserializeObject<CreateEmployeeDto>(json);
        //                    if (message.IsValid(schema))
        //                        client.CreateUser(CreateJson);
        //                    var LockJson = JsonConvert.DeserializeObject<LockUser>(json);
        //                    if (message.IsValid(schema1))
        //                        client.BlockUser(LockJson);
        //                    var ROleJson = JsonConvert.DeserializeObject<CreateUserRoleDto>(json);
        //                    if (message.IsValid(schema2))
        //                        client.CreateUserRole(ROleJson);
        //                }
        //                catch
        //                {
        //                  
        //                }
        //            }
        //        }
        //    }
        //}

        public static void ValidateAndSend(Queue<string> messages) 
        {
            while (true)
            {

                lock (messages)
                {

                    messageJson = messages.Dequeue();
                }

                var message = JObject.Parse(messageJson);
                var createJson = JsonConvert.DeserializeObject<CreateEmployeeDto>(messageJson);
                if (message.IsValid(schema))
                {
                    client.CreateUser(createJson);
                    continue;
                }

                var lockJson = JsonConvert.DeserializeObject<LockUser>(messageJson);
                if (message.IsValid(schema1))
                {
                    client.BlockUser(lockJson);
                    continue;
                }

                var roleJson = JsonConvert.DeserializeObject<CreateUserRoleDto>(messageJson);
                if (message.IsValid(schema2))
                {
                    client.CreateUserRole(roleJson);
                    continue;
                }


            }

        }
    }
}







