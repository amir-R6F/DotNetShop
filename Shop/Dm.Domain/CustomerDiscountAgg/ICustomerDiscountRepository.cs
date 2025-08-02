using System.Collections.Generic;
using Dm.Application.Contracts.CustomerDiscount;
using Shop.Domain;

namespace Dm.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IBaseRepository<long, CustomerDiscount>
    {
        EditCustomerDiscount GetDetails(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}