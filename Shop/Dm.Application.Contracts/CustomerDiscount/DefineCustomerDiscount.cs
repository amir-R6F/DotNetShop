namespace Dm.Application.Contracts.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        
        public long ProductId { get;  set; }
        public int DiscountRate { get;  set; }
        public string StartDate { get;  set; }
        public string EndDate { get;  set; }
        public bool Reason { get;  set; }
    }
}