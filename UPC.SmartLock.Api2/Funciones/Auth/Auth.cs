using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using UPC.SmartLock.Api.Aplicacion;
using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BL.Util.Interface;
using UPC.SmartLock.Configuration;
using UPC.SmartLock.BL.Auth;
using UPC.SmartLock.BE.Auth.Request;

namespace UPC.SmartLock.Api2.Funciones.Auth
{
    public class Auth : FunctionBase
    {
        private static RepositorioUPC _repositorioUpc = default(RepositorioUPC);
        private IAESEncriptacion _encriptacionService = default(IAESEncriptacion);

        public Auth(IAESEncriptacion encriptacionService, RepositorioUPC repositorioUpc)
        {
            _repositorioUpc = repositorioUpc;
            _encriptacionService = encriptacionService;
        }

        #region ejemplosMysql

        [Function("Logeo")]
        public async Task<IActionResult> Logeo([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/logeo")] HttpRequest req, ILogger log)
        {
            {
                try
                {
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    var request = JsonSerializer.Deserialize<AuthRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);

                    var blComercio = new AuthManager(repositorio, _encriptacionService);
                    var response = await blComercio.Login(request.Email, request.Password);

                    return FunctionBaseHttpMensaje.ResultadoObjeto(response);
                }
                catch (MensajeException mx)
                {
                    return FunctionBaseHttpMensaje.ResultadoMensaje(mx, "Function.Ose.DWH", true);
                }
                catch (Exception ex)
                {
                    return await FunctionBaseHttpMensaje.ResultadoErrorAsync(ex, "Function.Ose.DWH");
                }
            }
        }
        #endregion
    }
}
