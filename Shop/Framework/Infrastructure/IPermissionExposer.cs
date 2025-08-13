using System.Collections.Generic;

namespace Shop.Infrastructure
{

    public interface IPermissionExposer
    {
        Dictionary<string, List<PermissionDto>> Expose();
    }
}