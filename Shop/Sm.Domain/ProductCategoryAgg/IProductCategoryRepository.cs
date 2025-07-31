using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Shop.Domain;
using Sm.Application.Contracts.ProductCategory;

namespace Sm.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IBaseRepository<long, ProductCategory>
    {
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

        List<ProductCategoryViewModel> GetCategories();
    }
}