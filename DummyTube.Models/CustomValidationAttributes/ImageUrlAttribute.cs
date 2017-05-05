using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Models.CustomValidationAttributes
{
    public class ImageUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string url = value as string;
            WebRequest request;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
            }
            catch (Exception e)
            {
                return new ValidationResult(FormatErrorMessage(this.ErrorMessage));
            }

            using (var resp = request.GetResponse())
            {
                if (resp.ContentType.ToLower(CultureInfo.InvariantCulture)
                           .StartsWith("image/"))
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult(FormatErrorMessage(this.ErrorMessage));
            }
        }
    }
}
