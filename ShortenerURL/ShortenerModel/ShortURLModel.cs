using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShortenerModel
{
    public class ShortURLModel:MongoBaseModel
    {
        [BsonElement("ShortenedURL")]
        public string ShortenedURL { get; set; }

        [BsonElement("ActualURL")]
        public string ActualURL { get; set; }

        [BsonElement("TimesOfView")]
        public int TimesOfView { get; set; }

        [BsonElement("DateOfCreation")]
        public DateTime DateOfCreation { get; set; }


        
    }
}
