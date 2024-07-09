using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Freshlo.Common.Exceptions.EncryptionHelper
{
    public static class EncryptionHelper
    {
        public static string Encrypt(string encryptString)
        {
            string EncryptionKey = "AUTOMATEBUDDY@2018";
            byte[] encryptKey = { };
            byte[] iv = { 55, 34, 87, 64, 87, 195, 54, 21 };
            encryptKey = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByte = Encoding.UTF8.GetBytes(encryptString);
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream,
                        des.CreateEncryptor(encryptKey, iv),
                        CryptoStreamMode.Write))
                    {
                        cStream.Write(inputByte, 0, inputByte.Length);
                        cStream.FlushFinalBlock();
                        return ToUrlSafeBase64(mStream.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "AUTOMATEBUDDY@2018";
            byte[] decryptKey = { };
            byte[] iv = { 55, 34, 87, 64, 87, 195, 54, 21 };
            byte[] inputByte = new byte[cipherText.Length];
            decryptKey = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                inputByte = FromUrlSafeBase64(cipherText);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(decryptKey, iv), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByte, 0, inputByte.Length);
                        cs.FlushFinalBlock();
                        System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                        return encoding.GetString(ms.ToArray());
                    }
                }
            }
        }

        private static string ToUrlSafeBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_').Replace("=", "");
        }

        private static byte[] FromUrlSafeBase64(string s)
        {
            while (s.Length % 4 != 0)
                s += "=";
            s = s.Replace('-', '+').Replace('_', '/');
            return Convert.FromBase64String(s);
        }
    }
}
