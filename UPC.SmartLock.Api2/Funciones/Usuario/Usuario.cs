using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using UPC.SmartLock.Api.Aplicacion;
using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.BL.Users;
using UPC.SmartLock.Configuration;

namespace UPC.SmartLock.Api2.Funciones.Usuario
{
    public class Usuario : FunctionBase
    {
        private static RepositorioUPC _repositorioUpc = default(RepositorioUPC);

        public Usuario(RepositorioUPC repositorioUpc)
        {
            _repositorioUpc = repositorioUpc;
        }

        [Function("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/usuario")] HttpRequest req, ILogger log)
        {
            {
                try
                {
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    var comercioRequest = JsonSerializer.Deserialize<UsuarioRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);

                    var blComercio = new UserManager(repositorio);
                    await blComercio.CrearUsuario(comercioRequest);

                    return FunctionBaseHttpMensaje.ResultadoOk();
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

        [Function("ObtenerUsuarios")]
        public async Task<IActionResult> ObtenerUsuarios([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/usuario")] HttpRequest req, ILogger log)
        {
            {
                try
                {

                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);

                    var blComercio = new UserManager(repositorio);
                    var usuarios = await blComercio.ObtenerUsuario();

                    return FunctionBaseHttpMensaje.ResultadoObjeto(usuarios);
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

    }
}
