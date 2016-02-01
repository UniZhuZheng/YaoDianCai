using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Manager;
using Uni.YDC.Dao.Manager.Entity;

namespace Uni.YDC.Web.Manager.Service
{
    public class DishRecordService
    {
        /*
         * WebMenuRemote.AddDishRecord
         * */
        public static bool Insert(string BID, string dishNum, string dishName, float dishPrice, int dishCount) {
            try
            {
                return DishRecordDao.Insert(BID, dishNum, dishName, dishPrice, dishCount);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /*
         * BillService.AddBill
         * */
        public static bool Insert(string BID, List<DishRecordEntity> dre)
        {
            try
            {
                return DishRecordDao.Insert(BID, dre);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /*
         * BillService.ShowDishRecord
         * */
        public static List<DishRecordEntity> GetDishRecord(string bid)
        {
            return DishRecordDao.QueryAll(bid);
        }
        /*
         * WebMenuRemote.UpdateOrderDishStatus
         * */
        public static bool updateOrderDishStatus(string BID, string dishNum, string dishName, float dishPrice, int dishCount)
        {
            try
            {
                return DishRecordDao.updateOrderDishStatus(BID, dishNum, dishName, dishPrice, dishCount);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static string ToString(List<object[]> lists)
        {
            int size = lists.Count;
            if (size <= 0)
            {
                return "{\"ok\":false}";
            }
            string str = "{\"ok\":true,\"lists\":[";
            for (int i = 0; i < size; i++)
            {
                object[] obj = lists[i];
                str += "{";
                str += "\"id\":\"" + obj[0] + "\",";
                str += "\"dishNum\":\"" + obj[1] + "\",";
                str += "\"dishName\":\"" + obj[2] + "\",";
                str += "\"dishPrice\":\"" + obj[3] + "\",";
                str += "\"dishCount\":\"" + obj[4] + "\"";
                if (i == (size - 1))
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

        
    }
}
