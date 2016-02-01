using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityResponse
{
    /*
     * 响应消息之图文消息
     * */
    public class NewsMessage : BaseMessage
    {
        // 图文消息个数，限制为10条以内   
        private int articleCount;
        public int ArticleCount
        {
            get { return articleCount; }
            set { articleCount = value; }
        }
        // 多条图文消息信息，默认第一个item为大图   
        private List<ArticleEntity> articles;
        public List<ArticleEntity> Articles
        {
            get { return articles; }
            set { articles = value; }
        }

    }
}
