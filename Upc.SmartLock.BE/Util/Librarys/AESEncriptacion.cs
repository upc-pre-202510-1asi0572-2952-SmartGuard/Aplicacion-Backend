using System.Security.Cryptography;
using System.Text;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public class AESEncriptacion
    {
        private static byte[] _SALT_BYTES = new byte[8] { 43, 67, 216, 123, 145, 45, 87, 92 };

        private static byte[] _KEY_BYTES = new byte[8] { 35, 62, 235, 143, 53, 123, 211, 66 };

        private static AESEncriptacion _INSTANCIA;

        public static AESEncriptacion Instancia
        {
            get
            {
                if (_INSTANCIA == null)
                {
                    _INSTANCIA = new AESEncriptacion(_SALT_BYTES, _KEY_BYTES);
                }

                return _INSTANCIA;
            }
        }

        protected byte[] SaltBytes { get; set; }

        protected byte[] KeyBytes { get; set; }

        public byte[] Encriptar(byte[] buffer)
        {
            byte[] array = null;
            using MemoryStream memoryStream = new MemoryStream();
            using RijndaelManaged rijndaelManaged = new RijndaelManaged();
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(KeyBytes, SaltBytes, 1000);
            rijndaelManaged.KeySize = 256;
            rijndaelManaged.BlockSize = 128;
            rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
            rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
            rijndaelManaged.Mode = CipherMode.CBC;
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cryptoStream.Write(buffer, 0, buffer.Length);
                cryptoStream.Close();
            }

            return memoryStream.ToArray();
        }

        public byte[] Desencriptar(byte[] buffer)
        {
            byte[] array = null;
            using MemoryStream memoryStream = new MemoryStream();
            using RijndaelManaged rijndaelManaged = new RijndaelManaged();
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(KeyBytes, SaltBytes, 1000);
            rijndaelManaged.KeySize = 256;
            rijndaelManaged.BlockSize = 128;
            rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
            rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
            rijndaelManaged.Mode = CipherMode.CBC;
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cryptoStream.Write(buffer, 0, buffer.Length);
                cryptoStream.Close();
            }

            return memoryStream.ToArray();
        }

        public string ObtenerCadena(byte[] buffer)
        {
            return HexEncoding.GetString(Encriptar(buffer)).ToLower();
        }

        public byte[] ObtenerBuffer(string cadena)
        {
            return Desencriptar(HexEncoding.GetBytes(cadena));
        }

        public string EncriptarCadena(string cadena)
        {
            return ObtenerCadena(Constantes.Encoding.GetBytes(cadena));
        }

        public string DesencriptarCadena(string cadena)
        {
            return Constantes.Encoding.GetString(ObtenerBuffer(cadena));
        }

        public static void Instanciar(byte[] saltBytes = null, byte[] keyBytes = null)
        {
            _SALT_BYTES = saltBytes ?? _SALT_BYTES;
            _KEY_BYTES = keyBytes ?? _KEY_BYTES;
            _INSTANCIA = new AESEncriptacion(_SALT_BYTES, _KEY_BYTES);
        }

        public AESEncriptacion(byte[] saltBytes, byte[] keyBytes)
        {
            SaltBytes = saltBytes;
            KeyBytes = keyBytes;
        }
    }
}
