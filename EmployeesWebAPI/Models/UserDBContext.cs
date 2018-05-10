using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeesWebAPI.Models
{
    public class UserDBContext
    {
        public bool RegisterUser(User user)
        {
            bool userExists = false;
            using (PersonalEntities entities = new PersonalEntities())
            {
                var checkUserObj = entities.Users.Where(col => col.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (checkUserObj == null)
                {
                    User userObj = new User();
                    userObj.UserName = user.UserName;
                    userObj.Password = user.Password;
                    entities.Users.Add(userObj);
                    entities.SaveChanges();
                }
                else
                {
                    userExists = true;
                }
            }
            return userExists;
        }

        public void UpdateUserRoles(List<UserRoleMapping> userRolesMappingList)
        {
            using (PersonalEntities entities = new PersonalEntities())
            {
                int userId = userRolesMappingList.Select(col => col.UserId).First();
                List<UserRoleMapping> existingUserRoles = entities.UserRoleMappings.Where(col => col.UserId == userId).ToList();

                //Delete all existing roles
                foreach (var role in existingUserRoles)
                {
                    entities.UserRoleMappings.Remove(role);
                }

                if (!userRolesMappingList.Exists(col => string.IsNullOrWhiteSpace(col.RoleCode)))
                {
                    // Add new roles
                    foreach (var userRoleMap in userRolesMappingList)
                    {
                        UserRoleMapping userRoleMappingObj = new UserRoleMapping();
                        userRoleMappingObj.UserId = userRoleMap.UserId;
                        userRoleMappingObj.RoleCode = userRoleMap.RoleCode;
                        entities.UserRoleMappings.Add(userRoleMappingObj);
                    }
                }

                entities.SaveChanges();
            }
        }

        public UserDetails VerifyUser(User user)
        {
            UserDetails userDetailsObj = new UserDetails();
            using (PersonalEntities entities = new PersonalEntities())
            {
                var userObj = entities.Users.Where(obj => obj.UserName.Equals(user.UserName) && obj.Password.Equals(user.Password)).FirstOrDefault();
                if (userObj != null)
                {
                    userDetailsObj.IsUserExists = true;
                    userDetailsObj.UserId = userObj.UserId;
                    userDetailsObj.UserName = userObj.UserName;
                    userDetailsObj.UserRoles = entities.UserRoleMappings.Where(obj => obj.UserId == userObj.UserId).ToList();
                }
            }
            return userDetailsObj;
        }

        public List<UserRoleMapping> GetUserRoles(int userId)
        {
            List<UserRoleMapping> userRoles = new List<UserRoleMapping>();
            using (PersonalEntities entities = new PersonalEntities())
            {
                userRoles = entities.UserRoleMappings.Where(user => user.UserId == userId).ToList();
            }
            return userRoles;
        }

        public List<Role> GetRoles()
        {
            using (PersonalEntities entities = new PersonalEntities())
            {
                return entities.Roles.ToList();
            }
        }
    }
}