using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sm.Application.Contracts.Product;
using Sm.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Products
{
    public class Index : PageModel
    {
        [TempData]
        public string Message { get; set; }
        
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _categoryApplication;
        public SelectList Categories;
        public List<ProductViewModel> products;
        public ProductSearchModel SearchModel;


        public Index(IProductApplication productApplication, IProductCategoryApplication categoryApplication)
        {
            _productApplication = productApplication;
            _categoryApplication = categoryApplication;
        }

        public void OnGet(ProductSearchModel searchModel)
        {
            Categories = new SelectList(_categoryApplication.GetCategories(), "Id", "Name");
            products = _productApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                Categories = _categoryApplication.GetCategories()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
            var res = _productApplication.Create(command);
            return new JsonResult(res);
        }

        public IActionResult OnGetEdit(long id)
        {
            var product = _productApplication.GetDetails(id);
            product.Categories = _categoryApplication.GetCategories(); 
            return Partial("./Edit", product);
        }

        public JsonResult OnPostEdit(EditProduct command)
        {
            var res = _productApplication.Edit(command);
            return new JsonResult(res);
        }

    }
}