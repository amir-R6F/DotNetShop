using Sm.Application.Contracts.Product;

namespace Dm.Application.Contracts.ColleagueDiscount
{
    public class ColleagueDiscountViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
        public bool IsRemoved { get; set; }
        public string Product { get; set; }
    }
}