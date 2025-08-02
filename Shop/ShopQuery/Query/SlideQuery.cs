using System.Collections.Generic;
using System.Linq;
using ShopQuery.Contracts.Slide;
using SM.Infrastructure;

namespace ShopQuery.Contracts.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly SmContext _context;

        public SlideQuery(SmContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetSliders()
        {
            return _context.Sliders.Select(x => new SlideQueryModel
            {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                BtnText = x.BtnText,
                Heading = x.Heading,
                Text = x.Text,
                Title = x.Title,
                Link = x.Link
            }).ToList();
        }
    }
}