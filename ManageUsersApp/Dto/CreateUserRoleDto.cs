using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SenatApi;
using SenatApi.DTO;

namespace ManageUsersApp.Dto
{
    public class CreateUserRoleDto
    {
        public string UserId { get; set; }
        public EmployeeRefDto Person { get; set; }
        public RoleRefDto Role { get; set; }
        public CollegialBodyRefDto CollegialBOdy { get; set; }
        public CompanyRefDto Company { get; set; }
        public HoldingRefDto Holding { get; set; }
    }
}
