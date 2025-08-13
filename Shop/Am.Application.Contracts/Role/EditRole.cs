using System.Collections.Generic;
using Shop.Infrastructure;

namespace Am.Application.Contracts.Role
{
    public class EditRole : CreateRole
    {
        public long Id { get; set; }
        public List<PermissionDto> MappedPermissions { get; set; }
    }
}