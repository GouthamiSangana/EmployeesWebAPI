using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeesWebAPI.Models
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsUserExists { get; set; }
        public List<UserRoleMapping> UserRoles { get; set; }
    }
}