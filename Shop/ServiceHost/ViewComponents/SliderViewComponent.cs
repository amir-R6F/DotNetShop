using Microsoft.AspNetCore.Mvc;
using ShopQuery.Contracts.Slide;

namespace ServiceHost.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {

        private readonly ISlideQuery _query;

        public SliderViewComponent(ISlideQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var slides = _query.GetSliders();
            return View(slides);
        }
    }
}