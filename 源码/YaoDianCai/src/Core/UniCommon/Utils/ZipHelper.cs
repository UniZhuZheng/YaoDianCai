using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace Uni.Core.Common.Utils
{
    public class ZipHelper
    {
        //压缩文件
        public static void ZipFile(string FileToZip, string ZipedFile, int CompressionLevel, int BlockSize, string password)
        {
            //如果文件没有找到，则报错
            if (!System.IO.File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("The specified file " + FileToZip + " could not be found. Zipping aborderd");
            }
            System.IO.FileStream StreamToZip = new System.IO.FileStream(FileToZip, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.FileStream ZipFile = System.IO.File.Create(ZipedFile);
            ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
            ZipEntry ZipEntry = new ZipEntry("ZippedFile");
            ZipStream.PutNextEntry(ZipEntry);
            ZipStream.SetLevel(CompressionLevel);
            byte[] buffer = new byte[BlockSize];
            System.Int32 size = StreamToZip.Read(buffer, 0, buffer.Length);
            ZipStream.Write(buffer, 0, size);
            try
            {
                while (size < StreamToZip.Length)
                {
                    int sizeRead = StreamToZip.Read(buffer, 0, buffer.Length);
                    ZipStream.Write(buffer, 0, sizeRead);
                    size += sizeRead;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            ZipStream.Finish();
            ZipStream.Close();
            StreamToZip.Close();
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="dirPath">压缩文件夹的路径</param>
        /// <param name="fileName">生成的zip文件路径</param>
        /// <param name="level">压缩级别 0 - 9 0是存储级别 9是最大压缩</param>
        /// <param name="bufferSize">读取文件的缓冲区大小</param>
        public static void CompressDirectory(string dirPath,string fileName,int level,int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            using (ZipOutputStream s = new ZipOutputStream(File.Create(fileName)))
            {
                s.SetLevel(level);
                CompressDirectory(dirPath, dirPath, s, buffer);
                s.Finish();
                s.Close();
            }
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="root">压缩文件夹路径</param>
        /// <param name="path">压缩文件夹内当前要压缩的文件夹路径</param>
        /// <param name="s"></param>
        /// <param name="buffer">读取文件的缓冲区大小</param>
        private static void CompressDirectory(string root, string path, ZipOutputStream s, byte[] buffer)
        {
            //root = root.TrimEnd('/') + "//";
            string[] fileNames = Directory.GetFiles(path);
            string[] dirNames = Directory.GetDirectories(path);
            string relativePath = path.Replace(root, "");
            if (relativePath != "")
            {
                relativePath = relativePath.Replace("//", "/") + "/";
            }
            int sourceBytes;
            foreach (string file in fileNames)
            {

                ZipEntry entry = new ZipEntry(relativePath + Path.GetFileName(file));
                entry.DateTime = DateTime.Now;
                s.PutNextEntry(entry);
                using (FileStream fs=new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    do
                    {
                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
                        s.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);
                }
            }

            foreach (string dirName in dirNames)
            {
                string relativeDirPath = dirName.Replace(root, "");
                ZipEntry entry = new ZipEntry(relativeDirPath.Replace("//", "/") + "/");
                s.PutNextEntry(entry);
                CompressDirectory(root, dirName, s, buffer);
            }
        }

        /*
         * ShopService.CreateShop
         * */


        public static void UnZip(string FileToUpZip, string ZipedFolder)
        {
            if (!File.Exists(FileToUpZip))
            {
                return;
            }
            if (!Directory.Exists(ZipedFolder))
            {
                Directory.CreateDirectory(ZipedFolder);
            }
            ZipInputStream s = null;
            ZipEntry theEntry = null;
            string fileName;
            FileStream streamWriter = null;
            try
            {
                s = new ZipInputStream(File.OpenRead(FileToUpZip));
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.Name != String.Empty)
                    {
                        fileName = Path.Combine(ZipedFolder, theEntry.Name);
                        if (fileName.EndsWith("/") || fileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }
                        streamWriter = File.Create(fileName);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter = null;
                }
                if (theEntry != null)
                {
                    theEntry = null;
                }
                if (s != null)
                {
                    s.Close();
                    s = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
        }

        public static void ZipFileMain(string[] args)
        {
            //string[] filenames = Directory.GetFiles(args[0]);
            string[] filenames = new string[] { args[0] };
            Crc32 crc = new Crc32();
            ZipOutputStream s = new ZipOutputStream(File.Create(args[1]));
            s.SetLevel(6); // 0 - store only to 9 - means best compression
            foreach (string file in filenames)
            {
                //打开压缩文件
                FileStream fs = File.OpenRead(file);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                ZipEntry entry = new ZipEntry(file);
                entry.DateTime = DateTime.Now;
                // set Size and the crc, because the information
                // about the size and crc should be stored in the header
                // if it is not set it is automatically written in the footer.
                // (in this case size == crc == -1 in the header)
                // Some ZIP programs have problems with zip files that don't store
                // the size and crc in the header.
                entry.Size = fs.Length;
                fs.Close();
                crc.Reset();
                crc.Update(buffer);
                entry.Crc = crc.Value;
                s.PutNextEntry(entry);
                s.Write(buffer, 0, buffer.Length);
            }
            s.Finish();
            s.Close();
        }
    }
}
