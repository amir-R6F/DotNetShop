using System.Collections.Generic;
using Shop.Application;

namespace Sm.Application.Contracts.Slider
{
    public interface ISliderApplication
    {
        OperationResult Create(CreateSlider command);
        OperationResult Edit(EditSlider command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditSlider GetDetails(long id);
        List<SliderViewModel> GetList();
    }
}