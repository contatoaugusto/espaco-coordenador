using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Utilidades
{
    public class Utitilidades
    {


        public static Image ConvertByteToImage(byte[] bytes)
        {
            Image image = null;
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                image = Image.FromStream(stream);
            }
            return image;
        }

    }
}
