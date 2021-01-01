using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace ShortenerModel.Validation
{

    public class CustomValidation
    {
        public sealed class CheckUrlValid : ValidationAttribute
        {
            protected override ValidationResult IsValid(object Url, ValidationContext validationContext)
            {
                Uri uriResult;
                bool result = Uri.TryCreate(Url.ToString(), UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (result)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("This URL isn't valid,Please enter a valid one");
                }
            }
        }
    }
}

