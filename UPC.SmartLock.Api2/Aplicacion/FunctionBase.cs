using Microsoft.AspNetCore.Http;
using UPC.SmartLock.BE.Aplicacion;
using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.Api.Aplicacion
{
    public class FunctionBase
    {
        public TokenSesion Token { get; set; }
        public FunctionBase()
        {
            //DataAplicacion = new BE.DataAplicacion();
        }
        protected async ValueTask<bool> ValidarToken(HttpRequest req)
        {
            var objTokenUsuario = new TokenSesion();

            //var objTokenUsuario = new TokenSesion();
            //var cabecera = req.Headers[ConfiguracionApp.TOKEN_CABECERA];
            //var blSesion = default(Infraestructure.BL.Aplicacion.Impl.TokenSesion);

            //if (!string.IsNullOrEmpty(cabecera))
            //{
            //    //objTokenUsuario.CargarCadenaEncriptada(cabecera);

            //    blSesion = new Infraestructure.BL.Aplicacion.Impl.TokenSesion();

            //    return true;

            //    //if (blSesion.ValidarTokenAsync(objTokenUsuario))
            //    //{
            //    //    Token = objTokenUsuario;

            //    //    return true;
            //    //}
            //    //else
            //    //{
            //    //    throw new MensajeException("El Token enviado es inválido.");
            //    //}

            //}
            //else
            //{
            //    throw new MensajeException("El Token es obligatorio.");
            //}


            return true;
        }
        protected void ValidarIdentidadSolicitud(string rucComercioBody)
        {
            if (!rucComercioBody.Equals(Token.Ruc.ToString())) throw new MensajeException($"El RUC del emisor no esta autorizado para usar el servicio. Revise el TOKEN enviado.");
        }
    }
}
