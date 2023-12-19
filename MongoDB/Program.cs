using MongoDB.Driver;
using MongoDBProject;
using System.Data;
using System.Drawing;

string connectionString = "mongodb://127.0.0.1:27017";
string databaseName = "potato_db";
string collectionName = "picture";

var client = new MongoClient(connectionString);
var db = client.GetDatabase(databaseName);
var collection = db.GetCollection<PotatoModel>(collectionName);

var potato = new PotatoModel("Foto2", 2, 3, "Apel", "Warna", (Bitmap)Image.FromFile(@"F:\Download\F4NdwMwaQAEFC_e.bmp"), DateTime.Now);

await collection.InsertOneAsync(potato);

var results = await collection.FindAsync(_ => true);

foreach (var result in results.ToList())
{
    Console.WriteLine(value: $"{result.Title}: Terima{result.Accept}, Tolak{result.Reject}, Buahnya{result.Buah}, Warnanya{result.Warna}, Bitmap:{result.Bitmap}, Waktu:{result.Waktu}");
}