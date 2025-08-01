using System.Collections.Generic;
using Shop.Domain;
using Sm.Application.Contracts.ProductCategory;
using Sm.Application.Contracts.ProductPicture;

namespace Sm.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IBaseRepository<long, ProductPicture>
    {
        EditProductPicture GetDetails(long id);

        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}