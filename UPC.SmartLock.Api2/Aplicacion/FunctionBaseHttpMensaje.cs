using Microsoft.AspNetCore.Mvc;
using System.Net;
using UPC.SmartLock.BE.Aplicacion;
using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.Api.Aplicacion
{
    public class FunctionBaseHttpMensaje
    {
        private static IActionResult ResponseHTTP<T>(T value, int statusCode = (int)HttpStatusCode.OK, string contentType = "application/json")
        {
            return new ContentResult
            {
                Content = ConversorJson.ObtenerCadena(value),
                StatusCode = statusCode,
                ContentType = contentType
            };
        }
        public static async Task<IActionResult> ResultadoErrorAsync(Exception ex, string category)
        {
            var beResultado = new ResultadoError<object>();

            beResultado.CodigoError = await Logger.Instancia.EscribirErrorAsync(ex, category);
#if DEBUG
            beResultado.MensajeError = ex.Message;
#endif
            return ResponseHTTP(beResultado, (int)HttpStatusCode.InternalServerError);
        }

        public static IActionResult ResultadoMensaje(MensajeException ex, string category, bool generarLog = false)
        {
            var beResultado = new ResultadoError<object>();

            beResultado.CodigoError = ex.Codigo.ToString();
            beResultado.MensajeError = ex.Message;

            if (generarLog) Logger.Instancia.Escribir(LoggerLevel.ERRORES, ex, category);

            return ResponseHTTP(beResultado, (int)HttpStatusCode.BadRequest);
        }

        public static IActionResult ResultadoMensaje(MensajeExceptionExtendido ex, string category, bool generarLog = false)
        {
            var beResultado = new ResultadoError<object>();

            beResultado.CodigoError = ex.CodigoError.ToString();
            beResultado.MensajeError = ex.Message;

            if (generarLog) Logger.Instancia.Escribir(LoggerLevel.ERRORES, ex, category);

            return ResponseHTTP(beResultado, ex.Codigo);
        }

        public static IActionResult ResultadoObjeto<T>(T valor)
        {
            var beResultado = new ResultadoError<T>();
            return ResponseHTTP(valor);
        }
        public static IActionResult ResultadoMensaje(string codigo, string mensaje)
        {
            var beResultado = new ResultadoInformativo<object>();

            beResultado.Codigo = codigo ?? "0";
            beResultado.Mensaje = mensaje ?? "OK";

            return beResultado.ToOkObjectResult();
        }

        public static IActionResult ResultadoOk()
        {
            //var beResultado = new BE.Aplicacion.ResultadoWeb<object>();

            //beResultado.TieneError = false;
            //beResultado.Resultado = "OK";

            //return beResultado.ToOkObjectResult();
            return ResponseHTTP(default(object));
        }
    }
}
