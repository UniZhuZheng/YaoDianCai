using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;


namespace Uni.Core.Common.Cryptography
{
    public class Crypto
    {
        public static string GeneratePassword(int length)
        {
            string noise = "1234567890mnbasdflkjqwerpoiqweyuvcxnzhdkqpsdk";
            Random random = new Random();
            string pwd = string.Empty;
            while (0 < length--)
            {
                pwd += noise[random.Next(noise.Length)];
            }

            return pwd;
        }

        public static string Encrypt(string data)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(data)));
        }

        public static byte[] Encrypt(byte[] data)
        {
            Rijndael hasher = Rijndael.Create();
            hasher.Key = MachinePseudoKeys.GetMachineConstant(32);
            hasher.IV = new byte[hasher.BlockSize >> 3];

            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, hasher.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(data, 0, data.Length);
                cs.FlushFinalBlock();
                hasher.Clear();
                return ms.ToArray();
            }
        }

        public static string Decrypt(string data)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(data)));
        }

        public static byte[] Decrypt(byte[] data)
        {
            Rijndael hasher = Rijndael.Create();
            hasher.Key = MachinePseudoKeys.GetMachineConstant(32);
            hasher.IV = new byte[hasher.BlockSize >> 3];

            using (var ms = new MemoryStream(data))
            using (var cs = new CryptoStream(ms, hasher.CreateDecryptor(), CryptoStreamMode.Read))
            {
                byte[] buffer = new byte[data.Length];
                int size = cs.Read(buffer, 0, buffer.Length);
                hasher.Clear();
                byte[] newBuffer = new byte[size];
                Array.Copy(buffer, newBuffer, size);
                return newBuffer;
            }
        }

        public static string Write(string data)
        {
            Rijndael hasher = Rijndael.Create();
            hasher.Key = GetSK();
            hasher.IV = new byte[hasher.BlockSize >> 3];

            string result = null;

            using (var ms = new MemoryStream())
            using (var ss = new CryptoStream(ms, hasher.CreateEncryptor(), CryptoStreamMode.Write))
            {
                byte[] buffer = Encoding.Unicode.GetBytes(data);
                ss.Write(buffer, 0, buffer.Length);
                ss.FlushFinalBlock();
                hasher.Clear();
                result = Convert.ToBase64String(ms.ToArray());
            }

            return result;
        }

        public static byte[] Write(byte[] data)
        {
            Rijndael hasher = Rijndael.Create();
            hasher.Key = GetSK();
            hasher.IV = new byte[hasher.BlockSize >> 3];

            byte[] result = null;

            using (var ms = new MemoryStream())
            using (var ss = new CryptoStream(ms, hasher.CreateEncryptor(), CryptoStreamMode.Write))
            {
                byte[] buffer = data;
                ss.Write(buffer, 0, buffer.Length);
                ss.FlushFinalBlock();
                hasher.Clear();
                result = ms.ToArray();
            }
           
            return result;
        }

        public static string Read(string data)
        {
            Rijndael hasher = Rijndael.Create();
            hasher.Key = GetSK();
            hasher.IV = new byte[hasher.BlockSize >> 3];

            string result = null;
            byte[] bytes = Convert.FromBase64String(data);

            using (var ms = new MemoryStream(bytes))
            using (var ss = new CryptoStream(ms, hasher.CreateDecryptor(), CryptoStreamMode.Read))
            {
                byte[] buffer = new byte[bytes.Length];
                int size = ss.Read(buffer, 0, buffer.Length);
                hasher.Clear();
                byte[] newBuffer = new byte[size];
                Array.Copy(buffer, newBuffer, size);
                result = Encoding.Unicode.GetString(newBuffer);
            }

            return result;
        }

        public static byte[] Read(byte[] data)
        {
            Rijndael hasher = Rijndael.Create();
            hasher.Key = GetSK();
            hasher.IV = new byte[hasher.BlockSize >> 3];

            byte[] result = null;
            byte[] bytes = data;

            using (var ms = new MemoryStream(bytes))
            using (var ss = new CryptoStream(ms, hasher.CreateDecryptor(), CryptoStreamMode.Read))
            {
                byte[] buffer = new byte[bytes.Length];
                int size = ss.Read(buffer, 0, buffer.Length);
                hasher.Clear();
                byte[] newBuffer = new byte[size];
                Array.Copy(buffer, newBuffer, size);
                result = newBuffer;
            }

            return result;
        }

        private static byte[] GetSK()
        {
            Random random = new Random(7);
            byte[] randomKey = new byte[32];
            for (int i = 0; i < randomKey.Length; i++)
            {
                randomKey[i] = (byte)random.Next(byte.MaxValue);
            }
            return randomKey;
        }
    }
}
