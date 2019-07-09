using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageUsersApp.Dto
{
    public class EmployeeMultilingualTextDto
    {
        public Dictionary<string, string> FirstName { get; set; }
        public Dictionary<string, string> LastName { get; set; }
        public Dictionary<string,string> MiddleName { get; set; }
        public string ProfileUrl { get; set; }
    }
}
