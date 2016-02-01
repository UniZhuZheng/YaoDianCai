using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uni.YDC.Dao.Menu.Entity
{
    public class VideoEntity
    {
        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string tab;
        public string Tab
        {
            get { return tab; }
            set { tab = value; }
        }
        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private int sort;
        public int Sort
        {
            get { return sort; }
            set { sort = value; }
        }
        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}
