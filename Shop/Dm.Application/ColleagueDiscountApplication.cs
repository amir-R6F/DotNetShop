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
            var operation = new OperationResult();
            if (_colleagueDiscountRepository.Exists(x =>
                x.ProductId == command.ProductId &&
                x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.Duplicate);

            var colleaguediscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(colleaguediscount);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();

            var colleaguediscount = _colleagueDiscountRepository.Get(command.Id);

            if (colleaguediscount == null)
                return operation.Failed(ApplicationMessages.NotFound);
            
            if (_colleagueDiscountRepository.Exists(x =>
                x.ProductId == command.ProductId &&
                x.DiscountRate == command.DiscountRate &&
                x.Id != command.Id))
                return operation.Failed(ApplicationMessages.Duplicate);

            colleaguediscount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var colleaguediscount = _colleagueDiscountRepository.Get(id);

            if (colleaguediscount == null)
                return operation.Failed(ApplicationMessages.NotFound);

            colleaguediscount.Remove();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var colleaguediscount = _colleagueDiscountRepository.Get(id);

            if (colleaguediscount == null)
                return operation.Failed(ApplicationMessages.NotFound);

            colleaguediscount.Restore();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succedded();
        }
    }
}