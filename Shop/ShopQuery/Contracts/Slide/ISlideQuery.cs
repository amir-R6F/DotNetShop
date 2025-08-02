using System.Collections.Generic;

namespace ShopQuery.Contracts.Slide
{
    public interface ISlideQuery
    {
        List<SlideQueryModel> GetSliders();
    }
}