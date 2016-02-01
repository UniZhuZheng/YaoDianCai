using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace UniDllChart.Service
{
    public class AuthService
    {
        public static bool checkSignature(string signature, string timestamp, string nonce) {
            //加密/校验流程：  
            //1. 将token、timestamp、nonce三个参数进行字典序排序  
            string[] ArrTmp = { Global.token, timestamp, nonce };
            Array.Sort(ArrTmp);//字典排序  
            //2.将三个参数字符串拼接成一个字符串进行sha1加密  
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            //3.开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。  
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
