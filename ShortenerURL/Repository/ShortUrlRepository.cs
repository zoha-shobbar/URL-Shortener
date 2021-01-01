using MongoDB.Bson;
using MongoDB.Driver;
using ShortenerModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ShortUrlRepository : IShortUrlRepository
    {


        private readonly IMongoCollection<ShortURLModel> _mongoCollection;
        public ShortUrlRepository(string mongoDBConnectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(mongoDBConnectionString);
            var database = client.GetDatabase(dbName);
            _mongoCollection = database.GetCollection<ShortURLModel>(collectionName);
        }
        public IEnumerable<ShortURLModel> GetAllUrls()
        {

            return _mongoCollection.Find(x => true).ToList();
        }

        public ShortURLModel GetUrl(string shortUrl)
        {
            System.Linq.Expressions.Expression<Func<ShortURLModel, bool>> predicate = c => c.ShortenedURL == shortUrl;

            var q = _mongoCollection.Find(predicate).FirstOrDefault();
            return q;
        }


        public ShortURLModel GetByActualUrl(string longUrl)
        {
            System.Linq.Expressions.Expression<Func<ShortURLModel, bool>> predicate = c => c.ActualURL == longUrl;
            var q = _mongoCollection.Find(predicate).FirstOrDefault();
            return q;
        }

        public ShortURLModel Create(ShortURLModel model)
        {
            try
            {
                _mongoCollection.InsertOne(model);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }


        //public ShortURLModel Update(string id, ShortURLModel model)
        //{
        //    var docId = new ObjectId(id);
        //    _mongoCollection.ReplaceOne(m => m.Id == docId, model);
        //    return model;
        //}


        public ShortURLModel Update( ShortURLModel model)
        {

            _mongoCollection.ReplaceOne(m => m.Id == model.Id, model);
            return model;
        }
    }
}
