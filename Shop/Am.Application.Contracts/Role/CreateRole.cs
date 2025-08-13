using System.Collections.Generic;
using Shop.Infrastructure;

namespace Am.Application.Contracts.Role
{
    public class CreateRole
    {
        public string Name { get; set; }
        public List<int> Permissions { get; set; }
    }
}