namespace UPC.SmartLock.BE.Util.Librarys
{
    public class HexEncoding
    {
        public static string GetString(byte[] buffer, int offset, int length)
        {
            int num = offset + length;
            char[] array = new char[length * 2];
            int num2 = offset;
            int num3 = 0;
            while (num2 < num)
            {
                byte b = (byte)(buffer[num2] >> 4);
                array[num3] = (char)((b > 9) ? (b - 10 + 97) : (b + 48));
                b = (byte)(buffer[num2] & 0xFu);
                array[++num3] = (char)((b > 9) ? (b - 10 + 97) : (b + 48));
                num2++;
                num3++;
            }

            return new string(array);
        }

        public static string GetString(byte[] buffer)
        {
            char[] array = new char[buffer.Length * 2];
            int num = 0;
            int num2 = 0;
            while (num < buffer.Length)
            {
                byte b = (byte)(buffer[num] >> 4);
                array[num2] = (char)((b > 9) ? (b - 10 + 97) : (b + 48));
                b = (byte)(buffer[num] & 0xFu);
                array[++num2] = (char)((b > 9) ? (b - 10 + 97) : (b + 48));
                num++;
                num2++;
            }

            return new string(array);
        }

        public static byte[] GetBytes(string text)
        {
            byte[] array = new byte[text.Length / 2];
            char c = '\0';
            int num = 0;
            int num2 = 0;
            while (num < array.Length)
            {
                c = text[num2];
                array[num] = (byte)(((c > '9') ? ((c > 'Z') ? (c - 97 + 10) : (c - 65 + 10)) : (c - 48)) << 4);
                c = text[++num2];
                array[num] |= (byte)((c > '9') ? ((c > 'Z') ? (c - 97 + 10) : (c - 65 + 10)) : (c - 48));
                num++;
                num2++;
            }

            return array;
        }

        public static void GetBytes(string text, byte[] buffer, int offset)
        {
            char c = '\0';
            int num = Math.Min(offset + text.Length / 2, buffer.Length);
            int num2 = offset;
            int num3 = 0;
            while (num2 < num)
            {
                c = text[num3];
                buffer[num2] = (byte)(((c > '9') ? ((c > 'Z') ? (c - 97 + 10) : (c - 65 + 10)) : (c - 48)) << 4);
                c = text[++num3];
                buffer[num2] |= (byte)((c > '9') ? ((c > 'Z') ? (c - 97 + 10) : (c - 65 + 10)) : (c - 48));
                num2++;
                num3++;
            }
        }
    }
}
