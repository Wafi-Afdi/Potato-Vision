using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace MongoDBProject;

public class PotatoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id { get; set; }
    public string Name { get; set; }
    public int Accept { get; set; }
    public int Reject { get; set; }
    public int Total { get; set; }
    //string jenis buah
    //string target warna
    //string[] 
    //time tanggal (dari mongodb)
    //byte gambar

    public PotatoModel(string name, int accept, int reject) 
    {
        Name = name;
        Accept = accept;
        Reject = reject;
        //string 
        
    }

    //public string convertbitmap
    // convert dari bitmap ke a

}
