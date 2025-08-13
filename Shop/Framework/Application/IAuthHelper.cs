using System.Collections.Generic;

namespace Shop.Application
{
    public interface IAuthHelper
    {
        void SignIn(AuthViewModel account);

        bool IsAuthenticated();
        void SingOut();
        string CurrentAccountRole();

        AuthViewModel CurrentAccountInfo();

        List<int> GetPermissions();
    }
    
}