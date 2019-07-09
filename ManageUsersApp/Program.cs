using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManageUsersApp
{
    class Programapp
    {
        
      
        static void Main(string[] args)
        {
            using (new CycleCreator())
            {
                Console.ReadKey();
            }

        }
    }
}
