using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageUsersApp.Dto
{
    public class LockUser
    {
        public string Id { get; set; }
        public bool ForceLocked { get; set; }
    }
}
