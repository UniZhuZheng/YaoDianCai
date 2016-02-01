using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Dao.Menu;

namespace Uni.YDC.Web.Menu.Service
{
    public class VideoService
    {
        public static bool Insert(string SID,VideoEntity u) {
            return new VideoDao(SID).Insert(u);
        }
        public static bool Update(string SID, string name1, int sort1, string name2, int sort2)
        {
            return new VideoDao(SID).Update(name1,sort1,name2,sort2);
        }
        public static bool Update(string SID, string name, string fileName)
        {
            return new VideoDao(SID).Update(name,fileName);
        }
        public static bool Update(string SID, string name ,string number,string content,string tab)
        {
            return new VideoDao(SID).Update(name, number, content, tab);
        }
        public static bool Delete(string SID, string name)
        {
            return new VideoDao(SID).Delete(name);
        }
        public static int Count(string SID)
        {
            return new VideoDao(SID).Count();
        }
        public static VideoEntity QueryByName(string SID, string name)
        {
            return new VideoDao(SID).QueryByName(name);
        }
        public static List<VideoEntity> Query(string SID)
        {
            return new VideoDao(SID).Query();
        }

        public static VideoEntity QueryName(string SID, string name)
        {
            return new VideoDao(SID).QueryByName(name);
        }

        public static bool UpdateCount(string sid, string name, int count)
        {
            return new VideoDao(sid).Update(name, count);
        }
    }
}
