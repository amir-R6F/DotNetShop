using System.Collections.Generic;
using Am.Application.Contracts.Account;
using Shop.Domain;

namespace Am.Domain.AccountAgg
{
    public interface IAccountRepository : IBaseRepository<long, Account>
    {
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        EditAccount GetDetails(long id);
    }
}