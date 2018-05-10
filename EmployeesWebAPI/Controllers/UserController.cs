using EmployeesWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EmployeesWebAPI.Controllers
{
    public class UserController : ApiController
    {
        UserDBContext userDbContext = null;

        public UserController()
        {
            userDbContext = new UserDBContext();
        }

        [Route("api/User/Register")]
        [HttpPost]
        public IHttpActionResult Register(User user)
        {
            bool userExists = userDbContext.RegisterUser(user);
            if(userExists)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            return Ok();
        }

        [Route("api/User/UpdateUserRoles")]
        [HttpPost]
        public IHttpActionResult UpdateUserRoles(List<UserRoleMapping> userRolesMappingList)
        {
            userDbContext.UpdateUserRoles(userRolesMappingList);
            return Ok();
        }

        [Route("api/User/VerifyUser")]
        [HttpPost]
        public IHttpActionResult VerifyUser(User user)
        {
            UserDetails userDetails = userDbContext.VerifyUser(user);
            return Ok(userDetails);
        }

        [Route("api/User/GetUserRoles/{userId}")]
        [HttpGet]
        public IHttpActionResult GetUserRoles(int userId)
        {
            List<UserRoleMapping> userRoles = userDbContext.GetUserRoles(userId);
            return Ok(userRoles);
        }

        [Route("api/User/GetRoles")]
        [HttpGet]
        public IHttpActionResult GetAllRoles()
        {
            List<Role> roles = userDbContext.GetRoles();
            return Ok(roles);
        }
    }
}
