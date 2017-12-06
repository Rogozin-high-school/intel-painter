using System;
using System.Drawing;
using System.Net;
using System.IO;

class Utilities
{
    public static string ToBase64(Bitmap b)
    {
        //bitmap to byte array
        ImageConverter converter = new ImageConverter();
        byte[] byteArray = (byte[])converter.ConvertTo(b, typeof(byte[]));
        string base64String = Convert.ToBase64String(byteArray);
        return base64String;
    }

    public static Bitmap GetBitmapFromHttp(string url)
    {
        WebClient wc = new WebClient();
        Stream s = wc.OpenRead(url);
        Bitmap bmp = new Bitmap(s);
        return bmp;
    }
}