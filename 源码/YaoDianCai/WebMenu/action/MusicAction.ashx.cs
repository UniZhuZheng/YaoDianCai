using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Web.Menu.Service;
using System.IO;

namespace Uni.WebMenu.action
{
    /// <summary>
    /// MusicAction 的摘要说明
    /// </summary>
    public class MusicAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_Insert":
                    content = Insert(context);
                    break;
                case "OP_Update":
                    content = Update(context);
                    break;
                case "OP_UpdateInfo":
                    content = OP_UpdateInfo(context);
                    break;
                case "OP_UpdateCount":
                    content = UpdateCount(context);
                    break;
                case "OP_Delete":
                    content = Delete(context);
                    break;
                case "OP_Query":
                    content = Query(context);
                    break;
                case "OP_QueryName":
                    content = QueryName(context);
                    break;
                case "OP_Upload":
                    content = Upload(context);
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

        private string UpdateCount(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string name = context.Request.Params["name"];
            int count =Convert.ToInt32( context.Request.Params["count"]);
            if (!MusicService.UpdateCount(sid, name, count))
            {
                return "{\"ok\":false}";
            }
            return "{\"ok\":true}";
        }

        private string OP_UpdateInfo(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string number = context.Request.Params["number"];
            string name = context.Request.Params["name"];
            string tab = context.Request.Params["tab"];
            string content = context.Request.Params["content"];
            if (!MusicService.Update(sid, name, number, content, tab))
            {
                return "{\"ok\":false}";
            }
            return "{\"ok\":true}";
        }
        private string QueryName(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string name = context.Request.Params["name"];
            MusicEntity u = MusicService.QueryName(sid, name);

            if (u == null)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,";
            str += "\"number\":\"" + u.Number + "\",";
            str += "\"name\":\"" + u.Name + "\",";
            str += "\"tab\":\"" + u.Tab + "\",";
            str += "\"content\":\"" + u.Content + "\",";
            str += "\"count\":\"" + u.Count + "\",";
            str += "\"sort\":\"" + u.Sort + "\",";
            str += "\"fileName\":\"" + u.FileName + "\",";
            str += "\"createDate\":\"" + u.CreateDate + "\"";
            str += "}";
            return str;
        }
        private string Query(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            List<MusicEntity> list = MusicService.Query(sid);

            if (list.Count <= 0)
            {
                return "{\"ok\":false}";
            }

            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{";
                str += "\"number\":\"" + list[i].Number + "\",";
                str += "\"name\":\"" + list[i].Name + "\",";
                str += "\"tab\":\"" + list[i].Tab + "\",";
                str += "\"content\":\"" + list[i].Content + "\",";
                str += "\"count\":\"" + list[i].Count + "\",";
                str += "\"sort\":\"" + list[i].Sort + "\",";
                str += "\"fileName\":\"" + list[i].FileName + "\",";
                str += "\"createDate\":\"" + list[i].CreateDate + "\"";
                if (i == (list.Count - 1))
                {
                    str += "}";
                }
                else
                {
                    str += "},";
                }
            }
            str += "]}";
            return str;
        }

        private string Delete(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string name = context.Request.Params["name"];
            if (!MusicService.Delete(sid, name))
            {
                return "{\"ok\":false}";
            }
            return "{\"ok\":true}";
        }

        private string Update(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string name1 = context.Request.Params["name1"];
            string name2 = context.Request.Params["name2"];
            int sort1 = Convert.ToInt32(context.Request.Params["sort1"]);
            int sort2 = Convert.ToInt32(context.Request.Params["sort2"]);
            if (!MusicService.Update(sid, name1, sort1, name2, sort2))
            {
                return "{\"ok\":false}";
            }
            return "{\"ok\":true}";
        }

        private string Insert(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string number = context.Request.Params["number"];
            string name = context.Request.Params["name"];
            string tab = context.Request.Params["tab"];
            string content = context.Request.Params["content"];
            //判断视频是否存在
            if (MusicService.QueryByName(sid, name) != null)
            {
                return "{\"ok\":false,\"msg\":\"音乐名已存在，请重新填写\"}";
            }
            MusicEntity u = new MusicEntity();
            u.Number = number;
            u.Name = name;
            u.Tab = tab;
            u.Content = content;
            u.Count = 0;
            u.FileName = name + ".mp3";
            u.Sort = MusicService.Count(sid) + 1;

            if (MusicService.Insert(sid, u))
            {
                return "{\"ok\":true,\"msg\":\"音乐信息添加成功\"}";
            }
            else
            {
                return "{\"ok\":false,\"msg\":\"音乐信息添加失败\"}";
            }

        }
        private string Upload(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string name =context.Server.UrlDecode( context.Request.Params["name"]);

            if (IsPostFile())
            {
                return "{\"ok\":true,\"uploadMsg\":\"" + SaveRequestFiles(sid, name) + "\"}";
            }
            else
            {
                return "{\"ok\":true,\"uploadMsg\":\"上传文件不存在。\"}";
            }
        }

        private string SaveRequestFiles(string sid, string name)
        {
            string result = "";
            string LocalPath = HttpContext.Current.Server.MapPath("/shop/");
            //商家文件夹路径
            string folderpath = Path.Combine(LocalPath, sid, "Html", "ShopInfo", "music");
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            result = UploadImgToFile(sid, name, folderpath);
            return result;

        }

        private string UploadImgToFile(string sid, string name, string folderpath)
        {
            string result = "";
            if (Directory.Exists(folderpath))
            {
                int fCount = HttpContext.Current.Request.Files.Count;
                for (int i = 0; i < fCount; i++)
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(HttpContext.Current.Request.Files[i].FileName);
                    //文件名
                    string filename = file.Name;
                    //文件扩展
                    string fileextname = file.Extension.ToLower();
                    //文件类型
                    string filetype = HttpContext.Current.Request.Files[i].ContentType.ToLower();
                    //文件大小
                    int filesize = HttpContext.Current.Request.Files[i].ContentLength;
                    string newfilename = "";

                    //if (CheckFileExt(fileextname))
                    //{
                    //    newfilename = name + fileextname;
                    //    HttpContext.Current.Request.Files[i].SaveAs(folderpath + "/" + newfilename);
                    //    result = "文件上传成功。";
                    //}
                    //else
                    //{
                    //    //文件格式无效
                    //    result = "文件格式非法。";
                    //}

                    newfilename = name + fileextname;
                    HttpContext.Current.Request.Files[i].SaveAs(folderpath + "/" + newfilename);
                    result = "文件上传成功。";

                    //修改数据库表
                    MusicService.Update(sid, name, newfilename);
                }
                return result;
            }
            else
            {
                result = "文件对应路径不存在。";
                return result;
            }
        }
        /// <summary>
        /// 判断是否有上传的文件
        /// </summary>
        /// <returns>是否有上传的文件</returns>
        public static bool IsPostFile()
        {
            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                if (HttpContext.Current.Request.Files[i].FileName != "")
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 检查是否为合法的上传文件
        /// </summary>
        /// <param name="_fileExt"></param>
        /// <returns></returns>
        private bool CheckFileExt(string _fileExt)
        {
            string[] allowExt = new string[] { ".jpg" };
            for (int i = 0; i < allowExt.Length; i++)
            {
                if (allowExt[i].Equals(_fileExt)) { return true; }
            }
            return false;

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}