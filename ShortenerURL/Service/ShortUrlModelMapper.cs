using ShortenerModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class ShortUrlModelMapper
    {
        public static ShortURLModel MapRequestModelToDBModel(ShortURLRequestModel requestModel)
        {
            ShortURLModel result = new ShortURLModel
            {
                DateOfCreation = DateTime.Now,
                ActualURL = requestModel.LongURL
            };

            result.ShortenedURL = TokenGenerator.GenerateShortUrl();

            return result;
        }
    }
}
