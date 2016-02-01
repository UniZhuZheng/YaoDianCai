using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Web;
using System.Collections.Generic;
using Uni.YDC.Web.Menu.Service;
using Uni.YDC.Dao.Menu.Entity;


namespace Uni.WebMenu.Action
{
    public class DishAction : IHttpHandler
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            string option = context.Request.Params["option"];
            string content = "{\"ok\":false}";

            switch (option)
            {
                case "OP_ListDishType":
                    content = ListDishType(context);
                    break;

                case "OP_ListLimitDishes":  //menu_select
                    content = ListLimitDishes(context);
                    break;

                case "OP_CountLimitDishes": //menu_count
                    content = CountLimitDishes(context);
                    break;

                case "OP_ListLimitDishesSearch":  //menu_select
                    content = ListLimitDishesSearch(context);
                    break;

                case "OP_CountLimitDishesSearch": //menu_count
                    content = CountLimitDishesSearch(context);
                    break;
                case "OP_ShopListLimitDishes":  //menu_select
                    content = ShopListLimitDishes(context);
                    break;

                case "OP_ShopCountLimitDishes": //menu_count
                    content = ShopCountLimitDishes(context);
                    break;
                case "OP_CreateOneDish": //menu_add
                    content = CreateOneDish(context);
                    break;

                case "OP_RemoveDishByName": //menu_delete
                    content = RemoveDishByName(context);
                    break;

                case "OP_ChangeDishState": //menu_state
                    content = ChangeDishState(context);
                    break;

                case "OP_GetDishByName": //menu_select_name
                    content = GetDishByName(context);
                    break;

                case "OP_UpdateOneDish": //menu_update
                    content = UpdateOneDish(context);
                    break;

                case "OP_GetAllDishes": //menu_select_client
                    content = GetAllDishes(context);
                    break;

                case "OP_ImgUpload": //menu_img_upload
                    content = OP_ImgUpload(context);
                    break;  
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

        private string CountLimitDishesSearch(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string searchKey = context.Request.Params["searchKey"];
            int num = DishService.GetDishCount(sid, searchKey);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string ListLimitDishesSearch(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string searchKey = context.Request.Params["searchKey"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            return DishService.Select(sid, firstResult, maxResult, searchKey);
        }

        private string ShopCountLimitDishes(HttpContext context)
        {
            
            string sid = context.Request.Params["SID"];
            int state = 2;
            int num = DishService.GetDishCount(sid, state);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string ShopListLimitDishes(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int state = 2;
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);
            return DishService.Select(sid, state, firstResult, maxResult);
        }

        private string ListDishType(HttpContext context)
        {
            string sid = context.Request.Params["SID"];

            return DishService.GetAllTypeJson(sid);
        }

        private string ListLimitDishes(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            int firstResult = Convert.ToInt32(context.Request.Params["firstResult"]);
            int maxResult = Convert.ToInt32(context.Request.Params["maxResult"]);

            return DishService.Select(sid, firstResult, maxResult);
        }

        private string CountLimitDishes(HttpContext context)
        {
            string sid = context.Request.Params["SID"];

            int num = DishService.GetDishCount(sid);
            return "{\"ok\":true,\"count\":" + num + "}";
        }

        private string CreateOneDish(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string name = context.Request.Params["name"];
            string price = context.Request.Params["price"];
            string number = context.Request.Params["number"];
            string type = context.Request.Params["type"];
            string property = context.Request.Params["property"];
            string content = context.Request.Params["content"];

            DishEntity dish = new DishEntity
            {
                Name = name,
                Price = price,
                Number = number,
                Type = type,
                Property = property,
                Content = content,
                State = 0
            };

            if (!DishService.AddNewDish(sid, dish))
            {
                return "{\"ok\":false}";
            }

            return "{\"ok\":true}";
        }

        private string RemoveDishByName(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string name = context.Request.Params["name"];

            DishService.Delete(sid, name);
            return "{\"ok\":true}";
        }

        private string ChangeDishState(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string name = context.Request.Params["name"];
            int state = Convert.ToInt32(context.Request["state"]);

            DishService.StateChange(sid, name, state);
            return "{\"ok\":true}";
        }

        private string GetDishByName(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string name = context.Request.Params["name"];

            return DishService.GetDishJson(sid, name);
        }

        private string UpdateOneDish(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string number = context.Request.Params["number"];
            string name = context.Request.Params["name"];
            string property = context.Request.Params["property"];
            string type = context.Request.Params["type"];
            string price = context.Request.Params["price"];
            string content = context.Request.Params["content"];

            DishEntity dish = new DishEntity
            {
                Name = name,
                Price = price,
                Number = number,
                Type = type,
                Property = property,
                Content = content
            };

            DishService.Update(sid, dish);
            return "{\"ok\":true}";
        }

        private string GetAllDishes(HttpContext context)
        {
            string sid = context.Request.Params["SID"];

            return DishService.GetAllDishesJson(sid);
        }

        private string OP_ImgUpload(HttpContext context)
        {
            string sid = context.Request.Params["SID"];
            string name =context.Server.UrlDecode(context.Request.Params["name"]);

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
            string folderpath = Path.Combine(LocalPath, sid, "Html","ShopInfo", "menuimg");
            result= UploadImgToFile(sid, name,folderpath);
            if ("文件上传成功。".Equals(result))
            {
                folderpath = Path.Combine(LocalPath, sid, "Html", "NodePage", "public", "ShopInfo", "menuimg");
                return UploadImgToFile(sid, name, folderpath);
            }
            else {
                return result;
            }
            
        }

        private string UploadImgToFile(string sid, string name,string folderpath)
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

                    if (CheckFileExt(fileextname))
                    {
                        newfilename = name + fileextname;
                        HttpContext.Current.Request.Files[i].SaveAs(folderpath + "/" + newfilename);
                        result = "文件上传成功。";
                    }
                    else
                    {
                        //文件格式无效
                        result = "文件格式非法。";
                    }
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
                if (allowExt[i].Equals( _fileExt)) { return true; }
            }
            return false;

        }
    }
}