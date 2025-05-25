using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public static class FunctionExtensions
    {
        public static string GuardarError(this Exception ex)
        {
            return Logger.Instancia.EscribirError(ex, "Error");
        }

        public static async Task<T> ObtenerJsonAsync<T>(this HttpRequest req)
        {
            return await ConversorJson.DeserializarAsync<T>(req.Body);
        }

        public static IActionResult ToResult(this MensajeException mx)
        {
            return new ContentResult
            {
                Content = mx.Message,
                StatusCode = mx.Codigo,
                ContentType = "text/html"
            };
        }

        public static IActionResult ToResult(this Exception ex)
        {
            return new ContentResult
            {
                Content = ex.GuardarError(),
                StatusCode = 500,
                ContentType = "text/html"
            };
        }

        public static IActionResult OkResult()
        {
            return new ContentResult
            {
                Content = "OK",
                StatusCode = 200,
                ContentType = "text/html"
            };
        }

        public static IActionResult ToOkObjectResult<T>(this T value)
        {
            return new ContentResult
            {
                Content = ConversorJson.ObtenerCadena(value),
                StatusCode = 200,
                ContentType = "application/json"
            };
        }
    }
}
