using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Sm.Application.Contracts.Product;

namespace Sm.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        public long ProductId { get; set; }
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public bool IsRemoved { get; set; }
        
        public List<ProductViewModel> Products { get; set; }
    }
}