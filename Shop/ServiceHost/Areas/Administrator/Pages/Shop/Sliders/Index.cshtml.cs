using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sm.Application.Contracts.Product;
using Sm.Application.Contracts.ProductPicture;
using Sm.Application.Contracts.Slider;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Sliders
{
    public class Index : PageModel
    {
        [TempData]
        public string Message { get; set; }
        
        private readonly ISliderApplication _sliderApplication;
        public List<SliderViewModel> sliders;


        public Index(ISliderApplication sliderApplication)
        {
            _sliderApplication = sliderApplication;
        }

        public void OnGet()
        {
            sliders = _sliderApplication.GetList();
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateSlider();

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateSlider command)
        {
            var res = _sliderApplication.Create(command);
            return new JsonResult(res);
        }

        public IActionResult OnGetEdit(long id)
        {
            var sliderDetails = _sliderApplication.GetDetails(id);
            return Partial("./Edit", sliderDetails);
        }

        public JsonResult OnPostEdit(EditSlider command)
        {
            var res = _sliderApplication.Edit(command);
            return new JsonResult(res);
        }

        public IActionResult OnGetRemove(long id)
        {
            var res = _sliderApplication.Remove(id);
            if (res.IsSuccedded)
                return RedirectToPage("./index");

            Message = res.Message;
            return RedirectToPage("./index");

        }
        public IActionResult OnGetRestore(long id)
        {
            var res = _sliderApplication.Restore(id);
            if (res.IsSuccedded)
                return RedirectToPage("./index");

            Message = res.Message;
            return RedirectToPage("./index");
        }
    }
}