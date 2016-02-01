using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using UniDllChart.Service;

namespace WebChart.service
{
    /*
     * 文本信息处理程序
     * */
    public class MessageTextService
    {
        /*
         * MainAction.processRequest
         * */
        public static string ContentDeal(string openId,string reqContent)
        {
            string respContent="";
            
            if (reqContent.Equals("目录"))
            {
                string SID = WebChatService.IsExits(openId);
                if ("".Equals(SID)||SID==null) {
                    respContent = "请输入您的用户名和密码，\n格式：  DL:用户名,密码";
                } else {
                    respContent = GetContents();
                }
            }
            else if (Regex.IsMatch(reqContent, @"^DL:.+,.+$"))
            {
                //用户登录
                string contentTemp = reqContent.Split(':')[1];
                string userName = contentTemp.Split(',')[0];
                string password = contentTemp.Split(',')[1];

                string SID = WebChatService.IsExits(openId);
                if ("".Equals(SID) || SID == null)
                {
                    SID = WebChatService.Update(openId, userName, password);
                    if ("".Equals(SID) || SID == null)
                    {
                        respContent = "您输入的用户名和密码出错，请重新输入。\n格式：  DL:用户名,密码";
                    }
                    else
                    {
                        respContent = GetContents();
                    }
                }
                else 
                {
                    SID = WebChatService.Insert(openId, userName, password);
                    if ("".Equals(SID) || SID == null)
                    {
                        respContent = "您输入的用户名和密码出错，请重新输入。\n格式：  DL:用户名,密码";
                    }
                    else
                    {
                        respContent = GetContents();
                    }
                }
            }
            else if (reqContent.Equals("1"))
            {
                string SID = WebChatService.IsExits(openId);
                if ("".Equals(SID) || SID == null)
                {
                    respContent = "请输入您的用户名和密码，\n格式：  DL:用户名,密码";
                }
                else
                {
                    DateTime startDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    DateTime endDate = startDate.AddDays(1);
                    int[] counts = WebChatService.GetCountByDate(SID, startDate, endDate);
                    respContent = "当日下单数：" + counts[0] + "\n" +
                        "当日团购数：" + counts[1] + "\n" +
                        "当日认证数：" + counts[2] + "\n";
                }
            }
            else if (reqContent.Equals("2"))
            {
                string SID = WebChatService.IsExits(openId);
                if ("".Equals(SID) || SID == null)
                {
                    respContent = "请输入您的用户名和密码，\n格式：  DL:用户名,密码";
                }
                else
                {
                    DateTime startDate = Convert.ToDateTime((DateTime.Now.Year + "-" + DateTime.Now.Month + "-01"));
                    DateTime endDate = startDate.AddMonths(1);
                    int[] counts = WebChatService.GetCountByDate(SID, startDate, endDate);
                    respContent = "当月下单数：" + counts[0] + "\n" +
                        "当月团购数：" + counts[1] + "\n" +
                        "当月认证数：" + counts[2] + "\n";
                }
            }
            else if (reqContent.Equals("3"))
            {
                string SID = WebChatService.IsExits(openId);
                if ("".Equals(SID) || SID == null)
                {
                    respContent = "请输入您的用户名和密码，\n格式：  DL:用户名,密码";
                }
                else
                {
                    DateTime startDate = Convert.ToDateTime("2014-06-03");
                    DateTime endDate = DateTime.Now;
                    int[] counts = WebChatService.GetCountByDate(SID, startDate, endDate);
                    respContent = "全部下单数：" + counts[0] +"\n"+
                        "全部团购数：" + counts[1] + "\n" +
                        "全部认证数：" + counts[2] + "\n";
                }
            }
            else
            {
                string SID = WebChatService.IsExits(openId);
                if ("".Equals(SID) || SID == null)
                {
                    respContent = "请输入您的用户名和密码，\n格式：  DL:用户名,密码";
                }
                else
                {
                    respContent = GetContents();
                }
            }
            return respContent;
        }

        private static string GetContents()
        {
            return "您可以查询以下信息\n\n1:获取当日信息\n2:获取当月信息\n3:获取全部信息\n";
        }
    }
}