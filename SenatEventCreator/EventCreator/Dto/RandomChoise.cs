using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EventCreator.Dto;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;


namespace EventCreator.Dto
{
    public class RandomChoise
    {
        private static Random _random = new Random();
        private static Dictionary<string, string> _usersWithIdList = new Dictionary<string, string>()
       {
           {"1221fda4-32fd-471b-a4a7-88d9659b6460", "0fa7116f-b463-4724-a4d7-fbfa0bf179f2"},
           {"7e7a1bb8-27d9-492a-a2b4-724ad7ca22f1", "26f75200-3a46-4c02-95a0-971b491bb7eb"},
           {"719c6004-3457-42f1-a0d7-b6141e7fecf4", "be37ef08-507f-4dff-8851-d27b8670e8a2"},
           {"6da619bd-d004-4465-adfa-5cceffd49e41", "023de4d1-6a74-4b7a-a161-f387170e58d5"},
           {"ecdbaf77-ab8c-4a49-8be5-588f4cbd2c8f", "cd8242f4-ffa2-4812-abe2-f080287ea896"},
           {"de36b301-6e52-4571-aef9-b41e5ec30068", "8fd3aeba-63d9-4165-b708-a20559c7e3aa"},
           {"53cd3f2c-7e28-4c13-990a-36b743e9378e", "c9114cdc-6777-47d2-b62b-ceea3a98ae05"},
           {"c16bff4e-0b6d-4404-bfd0-83f92252201b", "79697ebd-4a22-4496-a23d-bd9f7087222d"},
           {"780103f2-4be3-4f86-be34-26a7e98b7e0e", "941c3130-e25e-4da5-8901-58e839a797bd"},
           {"859693ae-09cc-46fe-8568-66c8dce2ce9b", "49351b03-7c30-42cc-9ee7-a154ae530158"}
       };

        private static readonly List<string> _rolesCollegialBody = new List<string>()
       {
           "67b2f365-dea0-4ec8-94ba-0bbba442e082", "cf273f94-da97-47d3-ab3d-19d342321dc8",
           "87c52dfd-3e5b-45fb-9798-2b9449285379", "ca311823-c639-422a-87e6-38d347acbcbf",
           "a52d33a1-510d-4b8b-9721-76ce9960038d", "bc9269e7-fda7-4f28-bc99-7d8dc7a9347f",
           "1f450e87-aca6-4ab9-b45b-c9ef8f6000a6"
       };

        public static string CreateUser()
        {
            EmployeeMultilingualTextDto employee = new EmployeeMultilingualTextDto();
            employee.FirstName = new Dictionary<string, string> { { "ru-RU", "Vavava" + Guid.NewGuid() } };
            employee.LastName = new Dictionary<string, string>() { { "ru-RU", "Иванов" } };
            employee.MiddleName = new Dictionary<string, string>() { { "ru-RU", "ot4estvo" } };
            employee.ProfileUrl = null;
            CreateEmployeeDto empl = new CreateEmployeeDto();
            empl.Info = employee;
            string user = JsonConvert.SerializeObject(empl, Formatting.Indented);
            return user;
        }

        public static string AddRole()
        {
            var personalIds = _usersWithIdList.Keys.ToList();
            var person = personalIds[_random.Next(personalIds.Count)];
            var role = _rolesCollegialBody[_random.Next(_rolesCollegialBody.Count)];
            CreateUserRoleDto userRole = new CreateUserRoleDto();
            userRole.Holding = new HoldingRefDto() { Id = Guid.Parse("6f9e2038-ff68-4a37-8dd1-2e63ab7a0ad1") };
            userRole.Company = new CompanyRefDto() { id = Guid.Parse("4883d11b-7f34-4ff7-8919-c70b728ee5de") };
            userRole.CollegialBOdy = new CollegialBodyRefDto() { Id = Guid.Parse("1a12a08b-230c-4e87-a85a-de2796a30fa9") };
            userRole.Person = new EmployeeRefDto() { Id = new Guid(person) };
            userRole.UserId = _usersWithIdList[person];
            userRole.Role = new RoleRefDto() { Id = role };
            string JsonRole = JsonConvert.SerializeObject(userRole, Formatting.Indented);
            return JsonRole;
        }

        public static string BlockUser() 
        {
            LockUser blockUser = new LockUser();
            blockUser.Id = "c9114cdc-6777-47d2-b62b-ceea3a98ae05";
            blockUser.ForceLocked = true;
            string JsonBlock = JsonConvert.SerializeObject(blockUser, Formatting.Indented);
            return JsonBlock;
        }

        public static string SomethingActionsWithUsers()
        {
            var values = Enum.GetValues(typeof(ActionWithUser));
            var randomAction = (ActionWithUser)values.GetValue(_random.Next(values.Length));
            switch (randomAction)
            {
                case ActionWithUser.AddRole:
                    return AddRole();

                case ActionWithUser.AddUser:
                    return CreateUser();

                case ActionWithUser.BlockUser:
                    return BlockUser();

            }

            return string.Empty;
        }
    }
}
