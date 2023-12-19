using System;
using System.IO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Drawing;
using System.Drawing.Imaging;

namespace MongoDBProject;

public class PotatoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id { get; set; }
    public string Title { get; set; }
    public int Accept { get; set; }
    public int Reject { get; set; }
    public int Total { get; set; }
    
    public string Buah { get; set; }
    public string Warna { get; set; }
    public string Bitmap { get; set; }
    public DateTime Waktu { get; set; }
    
    public string convertbitmap(Bitmap bImage)
    {
        System.IO.MemoryStream ms = new MemoryStream();
        bImage.Save(ms, ImageFormat.Jpeg);
        byte[] byteImage = ms.ToArray();
        var SigBase64 = Convert.ToBase64String(byteImage);

        return SigBase64;
    }
    public PotatoModel(string title, int accept, int reject, string buah, string warna, Bitmap bitmap, DateTime waktu)
    {
        Title = title;
        Accept = accept;
        Reject = reject;
        Buah = buah;
        Warna = warna;
        Bitmap = convertbitmap(bitmap);
        Waktu = waktu;
    }
}
