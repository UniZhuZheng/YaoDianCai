using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Data.Common;
using Uni.Core.Database;
using Uni.Core.Database.Sql;

namespace Uni.YDC.Dao.Menu
{
    public class ShopDBFactory
    {
        /*
         * ShopService.CreateShop
         * */
        public static void Create(string dbFile, string SID)
        {
            SQLiteConnection.CreateFile(dbFile);

            SQLiteConnectionStringBuilder sqlitestr = new SQLiteConnectionStringBuilder();
            sqlitestr.DataSource = dbFile;

            ConnectionStringSettings connstring = new ConnectionStringSettings();
            connstring.ProviderName = "System.Data.SQLite";
            connstring.ConnectionString = sqlitestr.ToString();

            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(connstring.ProviderName);
            DbRegistry.RegisterDatabase(SID, providerFactory, connstring.ConnectionString);

            using (DbManager dbManeger = new DbManager(SID))
            {
                //商家信息表
                SqlCreate.Table mshopinfo = new SqlCreate.Table("mshopinfo")
                    .AddColumn("SID", System.Data.DbType.String, 100, false)
                    .AddColumn("account", System.Data.DbType.String, 100, false)
                    .AddColumn("name", System.Data.DbType.String, 100, false)
                    .AddColumn("hostId", System.Data.DbType.String, 100, false)
                    .AddColumn("phone", System.Data.DbType.String, 100, false)
                    .AddColumn("email", System.Data.DbType.String, 100, false)
                    .AddColumn("address", System.Data.DbType.String, 100, false)
                    .AddColumn("contact", System.Data.DbType.String, 100, false)
                    .AddColumn("createDate", System.Data.DbType.Date, 100, false);
                dbManeger.ExecuteNonQuery(mshopinfo);

                //桌号表
                SqlCreate.Table mtable = new SqlCreate.Table("mtable")
                    .AddColumn("name", System.Data.DbType.String, 100, false)//桌号名称
                    .AddColumn("BID", System.Data.DbType.String, 100, false);//订单号
                dbManeger.ExecuteNonQuery(mtable);

                //菜品表
                SqlCreate.Table mdish = new SqlCreate.Table("mdish")
                    .AddColumn("number", System.Data.DbType.String, 100, false)//菜品编号
                    .AddColumn("name", System.Data.DbType.String, 150, false)//菜品名
                    .AddColumn("price", System.Data.DbType.Double, 150, false)//菜品价格
                    .AddColumn("type", System.Data.DbType.String, 150, false)//菜品类型
                    .AddColumn("property", System.Data.DbType.String, 150, false)//菜品分类
                    .AddColumn("content", System.Data.DbType.String, 500, false)//菜品描述
                    .AddColumn("state", System.Data.DbType.Int32, false)//菜品状态 0销售 1停售 2下架
                    
                    .AddColumn("createDate", System.Data.DbType.Date, 300, false);//菜品添加时间
                dbManeger.ExecuteNonQuery(mdish);

                //订单表
                SqlCreate.Table mbill = new SqlCreate.Table("mbill")
                   .AddColumn("BID", System.Data.DbType.String, 100, false)//订单名称    
                   .AddColumn("tableName", System.Data.DbType.String, 100, false)//桌号
                   .AddColumn("totalCount", System.Data.DbType.String, 100, false)//订单总菜数
                   .AddColumn("totalPrice", System.Data.DbType.String, 100, false)//订单总价
                   .AddColumn("state", System.Data.DbType.Int32, 100, false)//菜单的状态
                   .AddColumn("memo", System.Data.DbType.String, 100, false)//菜单的备注
                   .AddColumn("createDate", System.Data.DbType.Date, 100, false);//下单时间
                dbManeger.ExecuteNonQuery(mbill);

                //订单子菜品表
                SqlCreate.Table morder = new SqlCreate.Table("morder")
                    .AddColumn("BID", System.Data.DbType.String, 100, false)//订单名称
                    .AddColumn("tableName", System.Data.DbType.String, 100, false)//桌号
                    .AddColumn("dishNumber", System.Data.DbType.String, 100, false)//订单子菜品编号
                    .AddColumn("dishName", System.Data.DbType.String, 100, false)//订单子菜品名
                    .AddColumn("dishPrice", System.Data.DbType.String, 100, false)//订单子菜品价格
                    .AddColumn("dishCount", System.Data.DbType.String, 100, false)//订单子菜品份数
                    .AddColumn("dishStatus", System.Data.DbType.Int32, false);//菜品是否上菜，0上菜，1未上菜
                dbManeger.ExecuteNonQuery(morder);

                //购物车表
                SqlCreate.Table mcart = new SqlCreate.Table("mcart")
                    .AddColumn("tableName", System.Data.DbType.String, 100, false)//桌号
                    .AddColumn("dishNumber", System.Data.DbType.String, 100, false)//菜品编号
                    .AddColumn("dishName", System.Data.DbType.String, 100, false)//菜品名称
                    .AddColumn("dishPrice", System.Data.DbType.String, 100, false)//菜品价格
                    .AddColumn("dishCount", System.Data.DbType.String, 100, false)//菜品份数
                    .AddColumn("dishStatus", System.Data.DbType.Int32, 100, false)//菜品的状态
                    .AddColumn("createDate", System.Data.DbType.String, 100, false);
                dbManeger.ExecuteNonQuery(mcart);

                //团购信息表
                SqlCreate.Table mtuan = new SqlCreate.Table("mtuan")
                    .AddColumn("tablename", System.Data.DbType.String, 100, false)//桌号 
                    .AddColumn("website", System.Data.DbType.String, 100, false)//团购来源
                    .AddColumn("number", System.Data.DbType.String, 100, false)//团购编号
                    .AddColumn("owner", System.Data.DbType.String, 100, false)//团购用户
                    .AddColumn("phone", System.Data.DbType.String, 100, false)//团购手机
                    .AddColumn("state", System.Data.DbType.Int32, 100, false)
                    .AddColumn("createDate", System.Data.DbType.DateTime, 100, false);//团购时间
                dbManeger.ExecuteNonQuery(mtuan);

                //呼叫信息表
                SqlCreate.Table mcall = new SqlCreate.Table("mcall")
                    .AddColumn("content", System.Data.DbType.String, 300, false)//呼叫内容 
                    .AddColumn("type", System.Data.DbType.Int32, 100, false)//呼叫类型
                    .AddColumn("state", System.Data.DbType.Int32, 100, false)//呼叫状态
                    .AddColumn("tableName", System.Data.DbType.String, 100, false)//呼叫所属桌号
                    .AddColumn("createDate", System.Data.DbType.DateTime, 100, false);//时间
                dbManeger.ExecuteNonQuery(mcall);

                //商家标签表
                SqlCreate.Table mshoplabel = new SqlCreate.Table("mshoplabel")
                    .AddColumn("SID", System.Data.DbType.String, 100, false)//商家编号 
                    .AddColumn("name", System.Data.DbType.String, 200, false)//商家标签内容
                    .AddColumn("type", System.Data.DbType.Int32, 100, false)//商家标签类型
                    .AddColumn("status", System.Data.DbType.Int32, 100, false)//商家标签状态
                    .AddColumn("count", System.Data.DbType.Int32, 100, false)//商家标签次数
                    .AddColumn("createDate", System.Data.DbType.DateTime, 100, false);//时间
                dbManeger.ExecuteNonQuery(mshoplabel);

                ////商家标签计数表
                //SqlCreate.Table mshoplabelcount = new SqlCreate.Table("mshoplabelcount")
                //    .AddColumn("SID", System.Data.DbType.String, 100, false)//商家编号 
                //    .AddColumn("name", System.Data.DbType.String, 100, false)//商家标签内容
                //    .AddColumn("count", System.Data.DbType.Int32, 100, false)//商家标签类型
                //    .AddColumn("lastDate", System.Data.DbType.DateTime, 100, false);//时间
                //dbManeger.ExecuteNonQuery(mshoplabelcount);

                //商家点评描述表
                SqlCreate.Table mshopcomment = new SqlCreate.Table("mshopcomment")
                    .AddColumn("SID", System.Data.DbType.String, 100, false)//商家编号 
                    .AddColumn("comment", System.Data.DbType.String, 500, false)//商家点评内容
                    .AddColumn("createDate", System.Data.DbType.DateTime, 100, false);//时间
                dbManeger.ExecuteNonQuery(mshopcomment);

                //视频管理表
                SqlCreate.Table mvideo = new SqlCreate.Table("mvideo")
                    .AddColumn("number", System.Data.DbType.String, 100, false)//视频编号 
                    .AddColumn("name", System.Data.DbType.String, 200, false)//视频名称
                    .AddColumn("tab", System.Data.DbType.String, 200, false)//视频标签
                    .AddColumn("content", System.Data.DbType.String, 200, false)//视频简介
                    .AddColumn("fileName", System.Data.DbType.String, 200, false)//视频文件名
                    .AddColumn("count", System.Data.DbType.Int32, 100, false)//视频点击数量
                    .AddColumn("sort", System.Data.DbType.Int32, 100, false)//视频排序
                    .AddColumn("createDate", System.Data.DbType.DateTime, 100, false);//上传时间
                dbManeger.ExecuteNonQuery(mvideo);
                //音乐管理表
                SqlCreate.Table mmusic = new SqlCreate.Table("mmusic")
                    .AddColumn("number", System.Data.DbType.String, 100, false)//音乐编号 
                    .AddColumn("name", System.Data.DbType.String, 200, false)//音乐名称
                    .AddColumn("tab", System.Data.DbType.String, 200, false)//音乐标签
                    .AddColumn("content", System.Data.DbType.String, 200, false)//音乐简介
                    .AddColumn("fileName", System.Data.DbType.String, 200, false)//音乐文件名
                    .AddColumn("count", System.Data.DbType.Int32, 100, false)//音乐点击数量
                    .AddColumn("sort", System.Data.DbType.Int32, 100, false)//音乐排序
                    .AddColumn("createDate", System.Data.DbType.DateTime, 100, false);//上传时间
                dbManeger.ExecuteNonQuery(mmusic);
            }
        }
        /*
         * ShopService.CreateShop
         * */
        public static void Register(string sid)
        {
            if (DbRegistry.IsDatabaseRegistered(sid))
            {
                return;
            }

            ConnectionStringSettings connstring = new ConnectionStringSettings();
            connstring.ProviderName = "System.Data.SQLite";
            SQLiteConnectionStringBuilder connstr = new SQLiteConnectionStringBuilder();
            connstr.DataSource = Path.Combine(HttpContext.Current.Server.MapPath("/shop/"), sid, "DB", sid + ".sqlite");
            connstring.ConnectionString = connstr.ToString();

            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(connstring.ProviderName);
            DbRegistry.RegisterDatabase(sid, providerFactory, connstring.ConnectionString);
        }
        /*
         * ShopService.CreateShop
         * */
        public static bool IsRegistered(string sid)
        {
            return DbRegistry.IsDatabaseRegistered(sid);
        }
    }
}
