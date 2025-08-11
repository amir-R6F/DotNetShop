using System.Collections.Generic;
using Shop.Application;

namespace Am.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Create(CreateAccount command);
        OperationResult Edit(EditAccount command);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        EditAccount GetDetails(long id);
        OperationResult ChangePassword(ChangePassword command);
        OperationResult Login(Login command);

        void Logout();

    }
}