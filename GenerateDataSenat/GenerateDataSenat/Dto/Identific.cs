using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateDataSenat.Dto
{
    public class Identific
    {
        public Guid Id { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }

    public class IssueVersionShort
    {
        public Identific Issue { get; set; }

        public override string ToString()
        {
            return Issue.ToString();
        }
    }
}
