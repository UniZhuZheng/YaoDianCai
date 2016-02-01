using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Uni.YDC.Web.Menu.Service;
using Uni.YDC.Dao.Menu.Entity;


namespace Uni.WebMenu.WebService
{
    [WebService(Namespace = "http://sz.yaodiancai.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WebManagerRemote : System.Web.Services.WebService
    {
        /*
         * WebManager.ShopService.CreateShop
         * */
        [WebMethod(Description = "创建商家文件夹目录和数据库")]
        public bool CreateShop(string sid, string account, string name, string hostId, string phone, string email, string address, string contact)
        {
            ShopInfoEntity si = new ShopInfoEntity
            {
                SID = sid,
                Account = account,
                Name = name,
                HostId = hostId,
                Phone = phone,
                Email = email,
                Address = address,
                Contact = contact,
                CreateDate = DateTime.Now
            };
            return ShopService.CreateShop(sid, si);
        }
        /*
         * WebManager.ShopService.SetShopBaseInfo
         * */
        [WebMethod(Description = "更新商家的基本信息")]
        public bool UpdateShopInfo(string sid, string phone, string email, string contact, string address)
        {
            try
            {
                ShopInfoService.SetShopInfo(sid, phone, email, contact, address);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        [WebMethod(Description = "添加商家的印象标签")]
        public bool ShopLabelInsert(string sid, string name, string type, string status)
        {
            try
            {
                ShopLabelEntity u = new ShopLabelEntity
                {
                    SID = sid,
                    Name = name,
                    Type = Convert.ToInt32(type),
                    Status = Convert.ToInt32(status),
                    Count = 0,
                    CreateDate = DateTime.Now
                };
                return ShopLabelService.ShopLabelInsert(u);
            }
            catch (Exception)
            {
                return false;
            }
        }
        [WebMethod(Description = "修改商家的印象标签")]
        public bool ShopLabelUpdate(string sid, string oldName, string newName)
        {
            try
            {

                return ShopLabelService.ShopLabelUpdate(sid, oldName, newName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod(Description = "删除商家的印象标签")]
        public bool ShopLabelDelete(string sid, string name)
        {
            try
            {

                return ShopLabelService.ShopLabelDelete(sid, name);
            }
            catch (Exception)
            {
                return false;
            }
        }

        //[WebMethod(Description = "更新商家的密码")]
        //public void UpdateShopPwd(string sid, string password)
        //{
        //    ShopInfoService.UpdateShopPwd(sid, password);
        //}
        //
        [WebMethod(Description = "更改商家的内网文件")]
        public bool InnerNetFileUpdate(string sid, string name, string domain, string ip, string port)
        {
            try
            {
                return InnerNetService.InnerNetFileUpdate(sid, name,domain, ip, port);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
