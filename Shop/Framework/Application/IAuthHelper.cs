namespace Shop.Application
{
    public interface IAuthHelper
    {
        void SignIn(AuthViewModel account);

        bool IsAuthenticated();
        void SingOut();
    }
    
}