using System.Collections.Generic;
using Am.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace Am.Application.Contracts.Account
{
    public class CreateAccount
    {
        public string Fullname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Mobile { get; set; }

        public long RoleId { get; set; }
        public string RoleName { get; set; }

        public IFormFile ProfilePhoto { get; set; }
        
        public List<RoleViewModel> roles { get; set; }


    }
}