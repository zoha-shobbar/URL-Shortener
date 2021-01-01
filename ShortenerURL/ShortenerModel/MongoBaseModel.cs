using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace ShortenerModel
{
    public class MongoBaseModel
    {
        public ObjectId Id { get; set; }
    }
}
