using System.Net;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public class MensajeExceptionExtendido : MensajeException
    {
        public int CodigoError { get; set; }
        public string Categoria { get; set; }

        public MensajeExceptionExtendido(string mensaje) : base(mensaje)
        {
            CodigoError = 0;
            Codigo = (int)HttpStatusCode.BadRequest;
            //Categoria = categoria;
        }

        public MensajeExceptionExtendido(string mensaje, string categoria, int codigoHttp = 400, int codigoError = 0) : base(mensaje, codigoHttp)
        {
            Categoria = categoria;
            CodigoError = codigoError;
        }

        public MensajeExceptionExtendido(string mensaje, HttpStatusCode codigoHttp = HttpStatusCode.BadRequest, int codigoError = 0) : base(mensaje, codigoHttp)
        {
            CodigoError = CodigoError;
        }
    }
}
