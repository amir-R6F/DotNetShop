using Microsoft.AspNetCore.Http;

namespace Shop.Application
{
    public interface IFileUploader
    {
        string Upload(IFormFile file, string path);
    }
}