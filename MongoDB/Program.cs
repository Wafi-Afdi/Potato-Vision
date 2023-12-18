using MongoDB.Driver;
using MongoDBProject;
using System.Data;

string connectionString = "mongodb://127.0.0.1:27017";
string databaseName = "potato_db";
string collectionName = "picture";

var client = new MongoClient(connectionString);
var db = client.GetDatabase(databaseName);
var collection = db.GetCollection<PotatoModel>(collectionName);



var potato = new PotatoModel("Foto2", 2, 3);

await collection.InsertOneAsync(potato);

var results = await collection.FindAsync(_ => true);

foreach (var result in results.ToList())
{
    Console.WriteLine(value: $"{result.Id}: {result.Name} {result.Accept} {result.Reject} {result.Total}");
}

//error = throw new Exception("Eror karena blablabla")