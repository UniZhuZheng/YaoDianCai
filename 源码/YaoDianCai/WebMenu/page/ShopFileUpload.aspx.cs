using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Uni.Core.Common.Utils;
using Uni.YDC.Dao.Menu;
using Uni.YDC.Web.Menu.Service;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Web.Menu;


namespace Uni.WebMenu.Page
{
    public partial class ShopFileUpload : System.Web.UI.Page
    {
        private string sid;
        private string modelName = Global.DefualtModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.Cookies["userInfo"] == null)
            //{
            //    Server.Transfer("RequestError.aspx");
            //    return;
            //}
            sid = HttpContext.Current.Request.Params["SID"];
            if (string.IsNullOrEmpty(sid))
            {
                Server.Transfer("RequestError.aspx");
                return;
            }

            int n = DishService.GetDishCount(sid);
            if (n > 0)
            {
                Server.Transfer("ShopDishList.aspx?SID=" + sid);
                return;
            }
        }
        
        //上传文件
        protected void btnupfile_Click(object sender, EventArgs e)
        {
            //获取商家名
            ShopInfoEntity si = ShopInfoService.GetShopInfo(sid);

            if (this.filepath.HasFile)
            {
                //获取上传文件名
                string filename = this.filepath.PostedFile.FileName;
                if (!si.Name.Equals(filename.Substring(0, filename.Length - 4)))
                {
                    this.uploadInfoNameImg.Style["background-position"] = "0 -18px";
                    this.uploadInfoNameInfo.Style["display"] = "none";
                    this.uploadInfoNameStatus.Style["display"] = "block";
                    return;
                }
                else
                {
                    this.uploadInfoNameImg.Style["background-position"] = "0 0";
                    this.uploadInfoNameInfo.InnerText = si.Name;
                    this.uploadInfoNameStatus.Style["display"] = "none";
                }

                string spath = Path.Combine(Global.ShopRootPath, sid);
                string unzippath = Path.Combine(spath, "Zip");
                string htmlpath = Path.Combine(spath, "Html");
                if (File.Exists(Path.Combine(unzippath, filename)))
                {
                    this.uploadInfoNameStatus.InnerText = "商家文件已存在";
                    this.uploadInfoNameImg.Style["background-position"] = "0 -18px";
                    this.uploadInfoNameInfo.Style["display"] = "none";
                    this.uploadInfoNameStatus.Style["display"] = "block";
                    return;
                }

                this.filepath.PostedFile.SaveAs(Path.Combine(unzippath, filename));

                string ziptemp = Path.Combine(unzippath, "temp");
                if (!Directory.Exists(ziptemp))
                {
                    Directory.CreateDirectory(ziptemp);
                }
                ZipHelper.UnZip(Path.Combine(unzippath, filename), ziptemp);

                if (ParseTableData(si, ziptemp))
                {
                    this.uploadInfoTableImg.Style["background-position"] = "0 0";
                    this.uploadInfoTableInfo.Style["display"] = "block";
                    this.uploadInfoTableStatus.Style["display"] = "none";
                }
                else
                {
                    this.uploadInfoTableImg.Style["background-position"] = "0 -18px";
                    this.uploadInfoTableInfo.Style["display"] = "none";
                    this.uploadInfoTableStatus.Style["display"] = "block";
                    deleteFile(unzippath);
                    TableService.DelAllTables(sid);
                    return;
                }

                if (ParseMenuData(si, ziptemp)) 
                {
                    this.uploadInfoDishImg.Style["background-position"] = "0 0";
                    this.uploadInfoDishInfo.Style["display"] = "block";
                    this.uploadInfoDishStatus.Style["display"] = "none";
                }
                else
                {
                    this.uploadInfoDishImg.Style["background-position"] = "0 -18px";
                    this.uploadInfoDishInfo.Style["display"] = "none";
                    this.uploadInfoDishStatus.Style["display"] = "block";
                    deleteFile(unzippath);
                    TableService.DelAllTables(sid);
                    DishService.Delete(sid);
                    return;
                }
                string zipmenu = Path.Combine(htmlpath,"ShopInfo", "menuimg");
                if (!Directory.Exists(zipmenu))
                {
                    Directory.CreateDirectory(zipmenu);
                }
                ZipHelper.UnZip(Path.Combine(unzippath, filename), zipmenu);

                string nodezipmenu = Path.Combine(htmlpath,"NodePage","public", "ShopInfo", "menuimg");
                if (!Directory.Exists(nodezipmenu))
                {
                    Directory.CreateDirectory(nodezipmenu);
                }
                ZipHelper.UnZip(Path.Combine(unzippath, filename), nodezipmenu);

                this.uploadInfoPicImg.Style["background-position"] = "0 0";
                this.uploadInfoPicInfo.Style["display"] = "block";
                this.uploadInfoPicStatus.Style["display"] = "none";
            }
            
        }

        private void deleteFile(string filePath)
        {
            foreach (string file in Directory.GetFiles(filePath))
            {
                if (File.Exists(file))
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.Attributes.ToString().IndexOf("ReadyOnly") >= 0)
                    {
                        fi.Attributes = FileAttributes.Normal;
                    }

                    File.Delete(file);
                }
            }
        }

        private bool ParseTableData(ShopInfoEntity si, string tmpath)
        {
            string filename = Path.Combine(tmpath, "桌号.xls");
            if (!File.Exists(filename))
            {
                return false;
            }

            DataTable dataTable = ExcelHelper.ExcelToData(filename, "桌号");
            if (dataTable == null || dataTable.Rows.Count <= 0 || !si.Name.Equals(dataTable.Rows[0][1].ToString()))
            {
                return false;
            }

            Dictionary<string, TableEntity> dic = new Dictionary<string,TableEntity>();
            DateTime dateNow = DateTime.Now;
            for (int i = 2; i < dataTable.Rows.Count; i++)
            {
                string tableName = dataTable.Rows[i][0].ToString();
                if (string.IsNullOrEmpty(tableName))
                {
                    break;
                }
                dateNow = dateNow.AddMinutes(1);
                TableEntity e = new TableEntity
                {
                    Name = tableName,
                    BID = sid+""+ dateNow.ToString("yyyyMMddhhmmss")
                };
                
                if (dic.ContainsKey(tableName))
                {
                    return false;
                }

                dic.Add(tableName, e);
            }

            return TableService.RefreshAll(si.SID, dic.Values.ToList());
        }

        private bool ParseMenuData(ShopInfoEntity si, string tmpath)
        {
            string filename = Path.Combine(tmpath, "菜品.xls");
            if (!File.Exists(filename))
            {
                return false;
            }

            DataTable dataTable = ExcelHelper.ExcelToData(filename, "菜品");
            if (dataTable == null || dataTable.Rows.Count <= 0 || !si.Name.Equals(dataTable.Rows[0][1].ToString()))
            {
                return false;
            }

            Dictionary<string, DishEntity> dic = new Dictionary<string, DishEntity>();
            List<string> menuName = new List<string>();
            for (int i = 2; i < dataTable.Rows.Count; i++)
            {
                if (!"".Equals(dataTable.Rows[i][0].ToString()))
                {
                    string dishName = dataTable.Rows[i][1].ToString();
                    if (string.IsNullOrEmpty(dishName))
                    {
                        return false;
                    }
                    string dishPrice = dataTable.Rows[i][2].ToString();
                    float price = 0;
                    if (string.IsNullOrEmpty(dishPrice) ||!float.TryParse(dishPrice,out price))
                    {
                        return false;
                    }
                    if (dic.ContainsKey(dishName))
                    {
                        return false;
                    }

                    DishEntity e = new DishEntity
                    {
                        Number = dataTable.Rows[i][0].ToString(),
                        Name = dataTable.Rows[i][1].ToString(),
                        Price = dataTable.Rows[i][2].ToString(),
                        Type = dataTable.Rows[i][3].ToString(),
                        Property = dataTable.Rows[i][4].ToString(),
                        Content = dataTable.Rows[i][5].ToString(),
                        State = Convert.ToInt32(dataTable.Rows[i][6].ToString())
                    };

                    dic.Add(dishName,e);
                }
            }
            return DishService.RefreshAll(si.SID, dic.Values.ToList());
        }

        protected void btnCreatMenu_Click(object sender, EventArgs e)
        {
            Server.Transfer("ShopDishList.aspx?SID=" + sid);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.opener=null;window.close();</script>");
        }

        protected void templateSelect_Click(object sender, EventArgs e) 
        {
            modelName = "model1";
        }
    }
}