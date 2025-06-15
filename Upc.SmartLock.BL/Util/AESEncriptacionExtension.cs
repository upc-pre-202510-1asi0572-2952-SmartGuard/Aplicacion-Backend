using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.BL.Util.Interface;

namespace UPC.SmartLock.BL.Util
{
    public class AESEncriptacionExtension : AESEncriptacion, IAESEncriptacion
    {
        public AESEncriptacionExtension(byte[] saltBytes, byte[] keyBytes) : base(saltBytes, keyBytes)
        {
        }
    }
}
