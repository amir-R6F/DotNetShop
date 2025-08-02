using System.Collections.Generic;
using Shop.Application;
using Shop.Domain;
using Sm.Application.Contracts.Slider;

namespace Sm.Domain.SliderAgg
{
    public interface ISliderRepository : IBaseRepository<long, Slider>
    {
        EditSlider GetDetails(long id);
        List<SliderViewModel> GetList();

    }
}