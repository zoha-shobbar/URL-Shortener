using Repository;
using ShortenerModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class ShortUrlServices : IShortUrlServices
    {
        private IShortUrlRepository _shortUrlRepository;
        public ShortUrlServices(IShortUrlRepository repository)
        {
            _shortUrlRepository = repository;
        }

        /// <summary>
        /// get all urls that are in the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ShortURLModel> GetAllUrls()
        {
            return _shortUrlRepository.GetAllUrls();
        }

        /// <summary>
        /// get the actual url base the short url of it
        /// </summary>
        /// <param name="shortUrl">short url for redirect to actual path</param>
        /// <returns></returns>
        public string GetUrl(string shortUrl)
        {

            var q = _shortUrlRepository.GetUrl(shortUrl);
            if (q != null)
            {
                q.TimesOfView++;
                _shortUrlRepository.Update(q);
                return q.ActualURL;
            }
            return null;

        }

        /// <summary>
        /// create new short url 
        /// </summary>
        /// <param name="model">most important thing is in the model is the ActualURL</param>
        /// <returns></returns>
        public ShortUrlResponseModel Create(ShortURLRequestModel model)
        {
            //check if the url saved perviously 
            ShortURLModel foundUrl = URLExist(model.ActualURL);
            if (foundUrl != null)
            {
                return new ShortUrlResponseModel { Model = foundUrl, Success = false, Message = "This url has been saved befor" };
            }

            //create new model to save in data base
            ShortURLModel shortURLModel = new ShortURLModel
            {
                DateOfCreation = DateTime.Now,
                ActualURL = model.ActualURL,
                //try to create a random alphanumeric short url 
                ShortenedURL = AutoURLGenerator.ShortUrlGenerator(8)
        };

            //save the model to  database
            var result = _shortUrlRepository.Create(shortURLModel);

            //return the result 
            return new ShortUrlResponseModel
            {
                Model = result,
                Success = true,
                Message = "Saved successfully"
            };


        }

        /// <summary>
        /// check if the url exist in the database
        /// </summary>
        /// <param name="url">the url that must be checked</param>
        /// <returns></returns>
        private ShortURLModel URLExist(string url)
        {
            ShortURLModel foundUrl = _shortUrlRepository.GetByActualUrl(url);
            if (foundUrl != null)
                return null;
            else
                return foundUrl;
        }
    }
}
