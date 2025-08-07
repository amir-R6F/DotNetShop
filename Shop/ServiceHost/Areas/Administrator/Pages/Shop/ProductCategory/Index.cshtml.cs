using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sm.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administrator.Pages.Shop.ProductCategory
{
    public class Index : PageModel
    {
        private readonly IProductCategoryApplication _productCategoryApplication;
        public List<ProductCategoryViewModel> productCategories;
        public ProductCategorySearchModel SearchModel;
        public EditProductCategory productCategory;

        public Index(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            productCategories = _productCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var res = _productCategoryApplication.Create(command);
            return new JsonResult(res);
        }

        public IActionResult OnGetEdit(long id)
        {
            productCategory = _productCategoryApplication.GetDetails(id);
            return Partial("./Edit", productCategory);
        }

        public JsonResult OnPostEdit(EditProductCategory command)
        {
            if (ModelState.IsValid)
            {
                // do something
                // this is for validation
            }
            
            var res = _productCategoryApplication.Edit(command);
            return new JsonResult(res);
        }
    }
}