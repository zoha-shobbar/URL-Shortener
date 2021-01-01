using ShortenerModel;
using System.Collections.Generic;

namespace Service
{
    public interface IShortUrlServices
    {
        ShortUrlResponseModel Create(ShortURLRequestModel model);
        IEnumerable<ShortURLModel> GetAllUrls();
        string GetUrl(string shortUrl);
    }
}