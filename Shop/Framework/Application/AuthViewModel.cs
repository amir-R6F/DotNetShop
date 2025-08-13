using System.Collections.Generic;

namespace Shop.Application
{
    public class AuthViewModel
    {

        public AuthViewModel()
        {
            
        }
        public AuthViewModel(long id, long roleId, string fullName, string username, List<int> permissions)
        {
            Id = id;
            RoleId = roleId;
            FullName = fullName;
            Username = username;
            Permissions = permissions;
        }

        public long Id { get; set; }
        public long RoleId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public List<int> Permissions { get; set; }
    }
}