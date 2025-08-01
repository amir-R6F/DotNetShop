using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sm.Application.Contracts.Product;
using Sm.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Administrator.Pages.Shop.ProductsPicture
{
    public class Index : PageModel
    {
        [TempData]
        public string Message { get; set; }
        
        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;
        public SelectList Products;
        public List<ProductPictureViewModel> pictures;
        public ProductPictureViewModel SearchModel;


        public Index(IProductApplication productApplication, IProductPictureApplication productPictureApplication)
        {
            _productApplication = productApplication;
            _productPictureApplication = productPictureApplication;
        }

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            pictures = _productPictureApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture()
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var res = _productPictureApplication.Create(command);
            return new JsonResult(res);
        }

        public IActionResult OnGetEdit(long id)
        {
            var PictureDetails = _productPictureApplication.GetDetails(id);
            PictureDetails.Products = _productApplication.GetProducts(); 
            return Partial("./Edit", PictureDetails);
        }

        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var res = _productPictureApplication.Edit(command);
            return new JsonResult(res);
        }

        public IActionResult OnGetRemove(long id)
        {
            var res = _productPictureApplication.Remove(id);
            if (res.IsSuccedded)
                return RedirectToPage("./index");

            Message = res.Message;
            return RedirectToPage("./index");

        }
        public IActionResult OnGetRestore(long id)
        {
            var res = _productPictureApplication.Restore(id);
            if (res.IsSuccedded)
                return RedirectToPage("./index");

            Message = res.Message;
            return RedirectToPage("./index");
        }
    }
}