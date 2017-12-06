using System;
using System.Drawing;

class Utilities
{
    public static string ToBase64(this Bitmap b)
    {
        //bitmap to byte array
        ImageConverter converter = new ImageConverter();
        byte[] byteArray = (byte[])converter.ConvertTo(b, typeof(byte[]));
        string base64String = Convert.ToBase64String(byteArray);
        return base64String;
    }
}