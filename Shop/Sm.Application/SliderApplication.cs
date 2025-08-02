using System.Collections.Generic;
using Shop.Application;
using Sm.Application.Contracts.Slider;
using Sm.Domain.SliderAgg;

namespace Sm.Application
{
    public class SliderApplication : ISliderApplication
    {
        private readonly ISliderRepository _sliderRepository;


        public SliderApplication(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public OperationResult Create(CreateSlider command)
        {
            var operation = new OperationResult();
            var slider = new Slider(command.Picture, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText);

            _sliderRepository.Create(slider);
            _sliderRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditSlider command)
        {
            var operation = new OperationResult();
            var slider = _sliderRepository.Get(command.Id);
            if (slider == null)
                return operation.Failed(ApplicationMessages.NotFound);

            slider.Edit(command.Picture, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText);

            _sliderRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slider = _sliderRepository.Get(id);
            if (slider == null)
                return operation.Failed(ApplicationMessages.NotFound);

            slider.Remove();

            _sliderRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slider = _sliderRepository.Get(id);
            if (slider == null)
                return operation.Failed(ApplicationMessages.NotFound);

            slider.Restore();

            _sliderRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditSlider GetDetails(long id)
        {
            return _sliderRepository.GetDetails(id);
        }

        public List<SliderViewModel> GetList()
        {
            return _sliderRepository.GetList();
        }
    }
}