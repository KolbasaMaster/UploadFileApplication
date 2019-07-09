using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateDataSenat.Dto
{
    public class CompanyPostDto
    {
        public Dictionary<string,string> Name { get; set; }
        public Dictionary<string, string> Holding { get; set; }
    }
}
