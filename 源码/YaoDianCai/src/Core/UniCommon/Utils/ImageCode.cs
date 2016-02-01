using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Web;

namespace Uni.Core.Common.Utils
{
    public class ImageCode
    {
        private static string LocalDBPath = HttpContext.Current.Server.MapPath("/shop/");
        // Convert Image to Byte[]
        public static byte[] ImageToByte(string SID, string menuname)
        {
            string file = Path.Combine(Path.Combine(LocalDBPath, SID, "Html", "ShopInfo", "menuimg", menuname + ".jpg"));
            if (!File.Exists(file))
            {
                return null;
            }
            Image image = Image.FromFile(file);
            
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        // Convert Byte[] to Image
        public static Image ByteToImage(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }

        // Convert Byte[] to a picture
        public static string CreateImageFromByte(string fileName, byte[] buffer)
        {
            string file = fileName; //文件名（不包含扩展名）
            Image image = ByteToImage(buffer);
            ImageFormat format = image.RawFormat;
            if (format.Equals(ImageFormat.Jpeg))
            {
                file += ".jpeg";
            }
            else if (format.Equals(ImageFormat.Png))
            {
                file += ".png";
            }
            else if (format.Equals(ImageFormat.Bmp))
            {
                file += ".bmp";
            }
            else if (format.Equals(ImageFormat.Gif))
            {
                file += ".gif";
            }
            else if (format.Equals(ImageFormat.Icon))
            {
                file += ".icon";
            }
            //文件路径目录必须存在，否则先用Directory创建目录
            File.WriteAllBytes(file, buffer);
            return file;
        }
    }
}
