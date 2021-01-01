using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static ShortenerModel.Validation.CustomValidation;

namespace ShortenerModel
{
    public class ShortURLRequestModel
    {
        [Required]
        [CheckUrlValid(ErrorMessage = "Please enter a valid Url")]
        public string ActualURL { get; set; }        
    }
}
