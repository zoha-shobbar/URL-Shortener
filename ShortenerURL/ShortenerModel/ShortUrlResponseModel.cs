using System;
using System.Collections.Generic;
using System.Text;

namespace ShortenerModel
{
  public  class ShortUrlResponseModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public ShortURLModel Model { get; set; }
    }
}
