using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Application;
using Shop.Infrastructure;
using Sm.Application.Contracts.Slider;
using Sm.Domain.SliderAgg;

namespace SM.Infrastructure.Repository
{
    public class SliderRepository : BaseRepository<long, Slider> , ISliderRepository
    {
        private readonly SmContext _context;
        
        public SliderRepository(SmContext context) : base(context)
        {
            _context = context;
        }

        public EditSlider GetDetails(long id)
        {
            return _context.Sliders.Select(x => new EditSlider
            {
                Id = x.Id,
                Heading = x.Heading,
                // Picture = x.Picture,
                Text = x.Text,
                Title = x.Title,
                BtnText = x.BtnText,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Link = x.Link
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SliderViewModel> GetList()
        {
            return _context.Sliders.Select(x => new SliderViewModel()
            {
                Id = x.Id,
                Heading = x.Heading,
                Picture = x.Picture,
                Title = x.Title,
                CreationDate = x.CreationDate.ToFarsi(),
                IsRemoved = x.IsRemoved
            }).ToList();
        }
    }
}