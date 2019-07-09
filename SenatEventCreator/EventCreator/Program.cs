using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCreator.Dto;

namespace EventCreator
{
    class Program
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
