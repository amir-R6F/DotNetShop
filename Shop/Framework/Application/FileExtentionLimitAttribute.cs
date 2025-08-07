using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Shop.Application
{
    public class FileExtentionLimitAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _ValidExtentions;

        public FileExtentionLimitAttribute(string[] validExtentions)
        {
            _ValidExtentions = validExtentions;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null) return true;
            var fileExtention = Path.GetExtension(file.FileName);
            return _ValidExtentions.Contains(fileExtention);


        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (!context.Attributes.ContainsKey("data-val"))
            {
                context.Attributes.Add("data-val", "true");
            }
            
            context.Attributes.Add("data-val-extentionLimit", ErrorMessage);
            
        }
    }
}