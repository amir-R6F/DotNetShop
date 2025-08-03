using System.Collections.Generic;
using System.Linq;
using Dm.Application.Contracts.ColleagueDiscount;
using Dm.Domain.ColleagueDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Sm.Domain.ProductAgg;
using SM.Infrastructure;

namespace Dm.Infrastructure.Repository
{
    public class ColleagueDiscountRepository : BaseRepository<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly SmContext _smContext;
        
        public ColleagueDiscountRepository(DiscountContext context, SmContext smContext) : base(context)
        {
            _context = context;
            _smContext = smContext;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _context.ColleagueDiscounts.Select(x => new EditColleagueDiscount
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _smContext.Products.Select(x => new {x.Id, x.Name});
            var query = _context.ColleagueDiscounts.Select(x => new ColleagueDiscountViewModel
            {
                Id = x.Id,
                IsRemoved = x.IsRemoved,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            var discounts = query.OrderByDescending(x => x.Id).ToList();
            
            
            discounts.ForEach(discount => 
                discount.Product = products.FirstOrDefault(x=> x.Id == discount.ProductId)?.Name);

            return discounts;
        }
    }
}