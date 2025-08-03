using System.Collections.Generic;
using Dm.Application.Contracts.ColleagueDiscount;
using Shop.Domain;

namespace Dm.Domain.ColleagueDiscountAgg
{
    public interface IColleagueDiscountRepository : IBaseRepository<long, ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}