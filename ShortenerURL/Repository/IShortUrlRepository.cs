using ShortenerModel;
using System.Collections.Generic;

namespace Repository
{
    public interface IShortUrlRepository
    {
        ShortURLModel Create(ShortURLModel model);
        IEnumerable<ShortURLModel> GetAllUrls();
        ShortURLModel GetByActualUrl(string longUrl);
        ShortURLModel GetUrl(string shortUrl);
        ShortURLModel Update(ShortURLModel model);
    }
}