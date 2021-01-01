using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service;
using ShortenerModel;

namespace ShortenerURL.Controllers
{
    public class ShortenerController : Controller
    {
        private readonly IShortUrlServices _shortUrlService;

        public ShortenerController(IShortUrlServices shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }


        /// <summary>
        /// the defualt get that redirect
        /// </summary>
        /// <param name="shorturl"></param>
        /// <returns></returns>
        [HttpGet("{shorturl}", Name = "Get")]
        public IActionResult Get(string shorturl)
        {
            string shortUrl = _shortUrlService.GetUrl(shorturl);

            if (shortUrl != null)
            {
                return Redirect(shortUrl);
            }

            return NotFound();
        }

        [HttpPost(nameof(Create))]
        public IActionResult Create([FromBody]ShortURLRequestModel model)
        {
            if (ModelState.IsValid)
            {
                ShortUrlResponseModel result = _shortUrlService.Create(model);
                if (result != null)
                    return Ok(result);
            }

            return BadRequest(ModelState.Values);
        }

        [HttpGet(nameof(GetAllUrls))]
        public IActionResult GetAllUrls()
        {
            try
            {
                var q = _shortUrlService.GetAllUrls().
                    Select(x => new
                    {
                        x.ActualURL,
                        x.DateOfCreation,
                        ShortenedURL = "https://pbid.io/" + x.ShortenedURL,
                        x.TimesOfView
                    }).OrderByDescending(x=>x.DateOfCreation);

                return Ok(q);
            }
            catch (Exception ex)
            {
                return NotFound();

            }
        }
    }
}