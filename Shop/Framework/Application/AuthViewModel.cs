namespace Shop.Application
{
    public class AuthViewModel
    {

        public AuthViewModel()
        {
            
        }
        public AuthViewModel(long id, long roleId, string fullName, string username)
        {
            Id = id;
            RoleId = roleId;
            FullName = fullName;
            Username = username;
        }

        public long Id { get; set; }
        public long RoleId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}