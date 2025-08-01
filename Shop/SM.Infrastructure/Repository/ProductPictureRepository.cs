using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Sm.Application.Contracts.ProductPicture;
using Sm.Domain.ProductPictureAgg;

namespace SM.Infrastructure.Repository
{
    public class ProductPictureRepository : BaseRepository<long, ProductPicture>, IProductPictureRepository
    {
        private readonly SmContext _context;

        public ProductPictureRepository(SmContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures.Select(x => new EditProductPicture
            {
                Id = x.Id,
                Picture = x.Picture,
                IsRemoved = x.IsRemoved,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures
                .Include(x=> x.Product)
                .Select(x => new ProductPictureViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                Product = x.Product.Name,
                CreationDate = x.CreationDate.ToString(),
                ProductId = x.ProductId,
                IsRemoved = x.IsRemoved
                
            });

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return query.OrderByDescending(x => x.Id).ToList();


        }
    }
}