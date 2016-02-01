using System;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Security.Cryptography;


namespace Uni.Core.Common.Cryptography
{
    public static class Signature
    {
        public static string Create<T>(T obj)
        {
            return Create(obj, Encoding.UTF8.GetString(MachinePseudoKeys.GetMachineConstant()));
        }

        public static string Create<T>(T obj, string secret)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(obj);
            string payload = GetHashBase64MD5(str + secret) + "?" + str;
            return HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(payload));
        }

        public static T Read<T>(string signature)
        {
            return Read<T>(signature, Encoding.UTF8.GetString(MachinePseudoKeys.GetMachineConstant()));
        }

        public static T Read<T>(string signature, string secret)
        {
            return Read<T>(signature, secret, true);
        }

        public static T Read<T>(string signature, string secret, bool useSecret)
        {
            try
            {
                string[] payloadParts = Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(signature)).Split('?');
                if (!useSecret || GetHashBase64(payloadParts[1] + secret) == payloadParts[0] || GetHashBase64MD5(payloadParts[1] + secret) == payloadParts[0])
                {
                    return new JavaScriptSerializer().Deserialize<T>(payloadParts[1]);
                }
            }
            catch (Exception)
            {
            }
            return default(T);
        }

        private static string GetHashBase64(string str)
        {
            return Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(str)));
        }

        private static string GetHashBase64MD5(string str)
        {
            return Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str)));
        }
    }
}
