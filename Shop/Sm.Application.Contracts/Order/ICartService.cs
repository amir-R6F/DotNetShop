namespace Sm.Application.Contracts.Order
{
    public interface ICartService
    {
        void Set(myCart cart);
        myCart Get();
    }
}