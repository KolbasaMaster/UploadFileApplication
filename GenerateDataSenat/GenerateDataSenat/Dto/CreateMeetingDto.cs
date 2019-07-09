using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateDataSenat.Dto
{
    public class CreateMeetingDto
    {
        public CollegialBodyRefDto CollegialBody { get; set; }
        public string Num { get; set; }
        public List<IssueRefDto> Issues { get; set; }
        public DateTime AgendaDueDate { get; set; }
        public DateTime MaterialsDueDate { get; set; }
        public string _discriminator { get; set; }
    }
}
