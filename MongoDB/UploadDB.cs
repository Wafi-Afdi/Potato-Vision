using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBProject
{
    public class UploadDB
    {
        private string connectionString = "mongodb://127.0.0.1:27017";
        private string databaseName = "potato_db";
        private string collectionName = "picture";

        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<PotatoModel> collection;
        public UploadDB() 
        {
            client = new MongoClient(connectionString);
            db =  client.GetDatabase(databaseName);
            collection = db.GetCollection<PotatoModel>(collectionName);
        }

        public async Task Create(PotatoModel potato)
        {
            try
            {
                await this.collection.InsertOneAsync(potato);
                // Optionally, you can work with the result here if needed
            }
            catch (Exception ex)
            {
                // You might choose to throw the exception again or handle it according to your application's logic
                throw;
            }
        }


    }
}
