using System.Net;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public class MensajeException : Exception
    {
        public int Codigo { get; set; }

        public MensajeException(string mensaje)
            : base(mensaje)
        {
        }

        public MensajeException(string mensaje, int codigo)
            : base(mensaje)
        {
            Codigo = codigo;
        }

        public MensajeException(string mensaje, HttpStatusCode codigo)
            : base(mensaje)
        {
            Codigo = (int)codigo;
        }
    }
}
