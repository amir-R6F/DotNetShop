using System.Collections.Generic;
using Shop.Infrastructure;

namespace Sm.Configuration.Permissions
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Product", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermission.ListProducts, "ListProducts"),
                        new PermissionDto(ShopPermission.SearchProducts, "SearchProducts"),
                        new PermissionDto(ShopPermission.CreateProducts, "CreateProducts"),
                        new PermissionDto(ShopPermission.EditProducts, "EditProducts"),
                    }
                },
                {
                    "ProductCategory", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermission.ListProductCategories, "ListProductCategories"),
                        new PermissionDto(ShopPermission.SearchProductCategories, "SearchProductCategories"),
                        new PermissionDto(ShopPermission.CreateProductCategories, "CreateProductCategories"),
                        new PermissionDto(ShopPermission.EditProductCategories, "EditProductCategories"),
                    }
                }
                
            };
        }
    }
}