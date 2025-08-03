using System.Collections.Generic;
using System.Runtime.InteropServices;
using Dm.Application.Contracts.ColleagueDiscount;
using Dm.Domain.ColleagueDiscountAgg;
using Shop.Application;

namespace Dm.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            throw new System.NotImplementedException();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            throw new System.NotImplementedException();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult Remove(long id)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult Restore(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}