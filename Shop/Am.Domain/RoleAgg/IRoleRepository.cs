using System.Collections.Generic;
using Am.Application.Contracts.Role;
using Shop.Domain;

namespace Am.Domain.RoleAgg
{
    public interface IRoleRepository : IBaseRepository<long, Role>
    {
        List<RoleViewModel> List();
        EditRole GetDetails(long id);
    }
}