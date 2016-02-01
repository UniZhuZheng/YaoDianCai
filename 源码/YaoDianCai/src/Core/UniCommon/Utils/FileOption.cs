using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Uni.Core.Common.Utils
{
    public class FileOption
    {
        /// <summary>
        /// 获取文件的内容
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetFileContent(string file)
        {

            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            StringBuilder output = new StringBuilder();
            string rl;
            while ((rl = sr.ReadLine()) != null)
            {
                output.Append(rl).Append("\n");
            }
            sr.Close();
            fs.Close();
            return output.ToString();
        }
        /// <summary>
        /// 写入文件 
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="FileSavePath"></param>
        public static void WriteFile(string Content, string FileSavePath)
        {
            if (File.Exists(FileSavePath))
            {
                File.Delete(FileSavePath);
            }
            FileStream fs = File.Create(FileSavePath);
            Byte[] bContent = System.Text.Encoding.UTF8.GetBytes(Content);
            fs.Write(bContent, 0, bContent.Length);
            fs.Close();
            fs.Dispose();
        }
        //创建文件
        public static bool FileCreate(string path)
        {
            bool ret = false;
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            if (!file.Exists)
            {
                file.Create();
                ret = true;
            }
            return ret;
        }
        //删除文件
        public static bool FileDelete(string path)
        {
            bool ret = false;
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                ret = true;
            }
            return ret;
        }

    }
}
