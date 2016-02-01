﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDllPCClient.EntityHelp
{
    public class DishEntity
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

        private string price;
        public string Price
        {
            get { return price; }
            set { price = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string property;
        public string Property
        {
            get { return property; }
            set { property = value; }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private int state;
        public int State
        {
            get { return state; }
            set { state = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        private string stateImg;
        public string StateImg
        {
            get { return stateImg; }
            set { stateImg = value; }
        }

        private string imgcode;
        public string Imgcode
        {
            get { return imgcode; }
            set { imgcode = value; }
        }
    }

    public class DishEntityList
    {
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private List<DishEntity> childmenu;
        public List<DishEntity> Childmenu
        {
            get { return childmenu; }
            set { childmenu = value; }
        }
    }

    public class PDishEntityList
    {
        private List<DishEntityList> dishentitylist;
        public List<DishEntityList> Dishentitylist
        {
            get { return dishentitylist; }
            set { dishentitylist = value; }
        }
        

    }
}
