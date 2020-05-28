using System;
using System.Text;
using System.Security.Cryptography;

namespace Segundo_parcial.Controlador
{
    public static class Encriptador
    {
        public static string CrearMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static bool CompararMD5(string cadena, string pMD5)
        {
            string hashOfInput = CrearMD5(cadena);
            
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return (0 == comparer.Compare(hashOfInput, pMD5));
        }
    }
}