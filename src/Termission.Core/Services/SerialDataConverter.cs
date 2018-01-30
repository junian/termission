using System;
using System.Collections.Generic;
using System.Text;

namespace Juniansoft.Termission.Core.Services
{
    /// <summary>
    /// Description of ComDataConverter.
    /// </summary>
    public class SerialDataConverter
    {
        public SerialDataConverter()
        {
        }

        public static byte[] IntToBytes(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }

        public static int BytesToInt(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static byte[] HexStringToBytes(string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
                return null;

            var sb = new StringBuilder();
            var hexStr = hexString.ToUpper();

            foreach (var c in hexStr)
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F'))
                    sb.Append(c);

            if ((sb.Length & 1) == 1)
                sb.Insert(0, '0');

            hexStr = sb.ToString();
            var bytes = new List<byte>();

            for (int i = 0; i < hexStr.Length; i += 2)
            {
                bytes.Add(
                    Convert.ToByte(
                        new StringBuilder()
                            .Append(hexStr[i])
                            .Append(hexStr[i + 1])
                            .ToString(), 16));
            }

            return bytes.ToArray();
        }

        public static string BytesToHexString(byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0)
                return string.Empty;
            return BitConverter.ToString(bytes);
        }

        public static byte[] StringToBytes(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        public static string BytesToString(byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0)
                return string.Empty;
            return System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }
    }
}
