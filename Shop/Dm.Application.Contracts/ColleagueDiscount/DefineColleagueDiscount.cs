using System.Collections.Generic;
using Sm.Application.Contracts.Product;

namespace Dm.Application.Contracts.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}