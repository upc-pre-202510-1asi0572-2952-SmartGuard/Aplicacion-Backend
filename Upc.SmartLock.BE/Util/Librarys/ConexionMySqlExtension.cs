using System.Data.Common;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public static class ConexionMySqlExtension
    {
        public static string GetGuidString(this byte[] valor)
        {
            return HexEncoding.GetString(valor).ToLower();
        }

        public static string GetGuidString(this DbDataReader lector, int ordinal)
        {
            if (lector.IsDBNull(ordinal))
            {
                return string.Empty;
            }

            byte[] array = new byte[16];
            lector.GetBytes(ordinal, 0L, array, 0, array.Length);
            return array.GetGuidString();
        }

        public static byte[] GuidFromString(this string valor)
        {
            return HexEncoding.GetBytes(valor);
        }

        public static MemoryStream GetMemoryStream(this DbDataReader lector, int ordinal)
        {
            if (lector.IsDBNull(ordinal))
            {
                return null;
            }

            byte[] array = new byte[1024];
            int num = array.Length;
            MemoryStream memoryStream = new MemoryStream();
            while (num == array.Length)
            {
                num = (int)lector.GetBytes(ordinal, 0L, array, 0, array.Length);
                memoryStream.Write(array, 0, num);
            }

            return memoryStream;
        }

        public static byte[] GetByteArray(this DbDataReader lector, int ordinal)
        {
            return lector.GetMemoryStream(ordinal)?.ToArray();
        }
    }
}
