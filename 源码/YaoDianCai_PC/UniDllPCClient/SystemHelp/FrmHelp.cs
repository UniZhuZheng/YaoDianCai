using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDllPCClient.SystemHelp
{
    /// <summary>
    /// 定义发布消息的委托
    /// </summary>
    /// <param name="sender">发布者</param>
    /// <param name="msg">消息</param>
    public delegate void SendHandler(object sender, object msg);
    public class FromHelp
    {
        /// <summary>
        ///消息发布的事件
        /// </summary>
        public static event SendHandler eventSend;

        public static void SendMessage(object sender, object msg)
        {
            if (eventSend != null)
            {
                eventSend(sender, msg);
            }
        }
    }

    public enum eControl
    {
        /// <summary>
        /// 显示提示窗口
        /// </summary>
        Show_Tip,
        /// <summary>
        /// 显示主窗口
        /// </summary>
        Show_Main,
        /// <summary>
        /// 加载新订单信息
        /// </summary>
        Load_NewData,
        /// <summary>
        /// 加载新团购信息
        /// </summary>
        Load_NewGroupBuy,
        /// <summary>
        /// 加载新订单信息
        /// </summary>
        Load_IntervalInfo,
    }

}
