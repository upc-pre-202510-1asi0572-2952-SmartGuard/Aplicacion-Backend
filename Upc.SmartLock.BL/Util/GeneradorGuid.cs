using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.BL.Util
{
    public static class GeneradorGuid
    {
        public static string NuevoGuid()
        {
            return HexEncoding.GetString(Guid.NewGuid().ToByteArray()).ToLower();
        }

        public static int NuevoId()
        {
            Random rnd = new Random();
            int id = rnd.Next(1, 1000);
            return id;
        }
    }
}
