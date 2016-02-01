using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Linq;


namespace Uni.Core.Common.Cryptography
{
    public static class MachinePseudoKeys
    {
        public static byte[] GetMachineConstant()
        {
            FileInfo fi = new FileInfo(typeof(MachinePseudoKeys).Assembly.Location);
            return BitConverter.GetBytes(fi.CreationTime.ToOADate());
        }

        public static byte[] GetMachineConstant(int bytesCount)
        {
            byte[] cnst = Enumerable.Repeat<byte>(0, sizeof(int)).Concat(GetMachineConstant()).ToArray();
            int icnst = BitConverter.ToInt32(cnst, cnst.Length - sizeof (int));
            var rnd = new Random(icnst);
            var buff = new byte[bytesCount];
            rnd.NextBytes(buff);
            return buff;
        }
    }
}