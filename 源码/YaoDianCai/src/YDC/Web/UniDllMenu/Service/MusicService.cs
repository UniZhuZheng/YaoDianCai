using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uni.YDC.Dao.Menu.Entity;
using Uni.YDC.Dao.Menu;

namespace Uni.YDC.Web.Menu.Service
{
    public class MusicService
    {
        public static bool Insert(string SID, MusicEntity u)
        {
            return new MusicDao(SID).Insert(u);
        }
        public static bool Update(string SID, string name1, int sort1, string name2, int sort2)
        {
            return new MusicDao(SID).Update(name1, sort1, name2, sort2);
        }
        public static bool Update(string SID, string name, string fileName)
        {
            return new MusicDao(SID).Update(name, fileName);
        }
        public static bool Update(string SID, string name, string number, string content, string tab)
        {
            return new MusicDao(SID).Update(name, number, content, tab);
        }
        public static bool Delete(string SID, string name)
        {
            return new MusicDao(SID).Delete(name);
        }
        public static int Count(string SID)
        {
            return new MusicDao(SID).Count();
        }
        public static MusicEntity QueryByName(string SID, string name)
        {
            return new MusicDao(SID).QueryByName(name);
        }
        public static List<MusicEntity> Query(string SID)
        {
            return new MusicDao(SID).Query();
        }

        public static MusicEntity QueryName(string SID, string name)
        {
            return new MusicDao(SID).QueryByName(name);
        }

        public static bool UpdateCount(string sid, string name, int count)
        {
            return new MusicDao(sid).Update(name, count);
        }
    }
}
