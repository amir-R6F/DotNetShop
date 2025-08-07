using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Shop.Application;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHost;

        public FileUploader(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public string Upload(IFormFile file, string path)
        {
            if (file == null) return "";

            var directoryPath = $"{_webHost.WebRootPath}//productCategories//{path}";

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);


            var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
            var filepath = $"{directoryPath}//{fileName}";
            using (var output = System.IO.File.Create(filepath))
                file.CopyTo(output);
            
            return $"{path}/{fileName}";
        }
    }
}