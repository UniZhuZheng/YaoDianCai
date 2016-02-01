using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityResponse
{
    /*
     * 图文消息中Article类的定义
     * */
    public class ArticleEntity
    {
        // 图文消息名称   
	    private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
	    // 图文消息描述   
	    private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
	    // 图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80，限制图片链接的域名需要与开发者填写的基本资料中的Url一致   
	    private string picUrl;
        public string PicUrl
        {
            get { return picUrl; }
            set { picUrl = value; }
        }
	    // 点击图文消息跳转链接   
	    private string url;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

    }
}
