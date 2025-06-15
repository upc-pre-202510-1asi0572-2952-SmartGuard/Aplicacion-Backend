using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using UPC.SmartLock.Api.Aplicacion;
using UPC.SmartLock.BE.Mienbros.Request;
using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.BL.Homes;
using UPC.SmartLock.BL.Miembros;
using UPC.SmartLock.BL.Users;
using UPC.SmartLock.BL.Util.Interface;
using UPC.SmartLock.Configuration;

namespace UPC.SmartLock.Api2.Funciones.Usuario
{
    public class Usuario : FunctionBase
    {
        private static RepositorioUPC _repositorioUpc = default(RepositorioUPC);
        private IAESEncriptacion _encriptacionService = default(IAESEncriptacion);

        public Usuario(IAESEncriptacion encriptacionService, RepositorioUPC repositorioUpc)
        {
            _repositorioUpc = repositorioUpc;
            _encriptacionService = encriptacionService;
        }

        #region ejemplosMysql

        [Function("CrearUsuarioMysql")]
        public async Task<IActionResult> CrearUsuarioMysql([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/usuarioMysql")] HttpRequest req, ILogger log)
        {
            {
                try
                {
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    var comercioRequest = JsonSerializer.Deserialize<UsuarioRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);

                    var blComercio = new UserManager(repositorio, _encriptacionService);
                   var user = await blComercio.CrearUsuario(comercioRequest);

                    return FunctionBaseHttpMensaje.ResultadoObjeto(user);
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

        [Function("ObtenerDispositivosXUsuario")]
        public async Task<IActionResult> ObtenerDispositivosXUsuario([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/dispositivos/{nickname}")] HttpRequest req, string nickname, ILogger log)
        {
            try
            {
                var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                var blComercio = new UserManager(repositorio, _encriptacionService);
                var dispositivos = await blComercio.obtenerDispositivosXUsuario(nickname);

                return FunctionBaseHttpMensaje.ResultadoObjeto(dispositivos);
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


        [Function("ActualizarPerfilUsuarioMysql")]
        public async Task<IActionResult> ActualizarPerfilUsuarioMysql(
[HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v1/usuarioMysql")] HttpRequest req,
ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var request = JsonSerializer.Deserialize<PerfilUsuarioRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                var blComercio = new UserManager(repositorio, _encriptacionService);

                var usuario = await blComercio.ActualizarPerfilUsuario(request);
                
                
                return FunctionBaseHttpMensaje.ResultadoObjeto(usuario);
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

        [Function("EliminarUsuarioPorIdMysql")]
        public async Task<IActionResult> EliminarUsuarioPorIdMysql(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/usuarioMysql/{userId}")] HttpRequest req,
        string userId,
        ILogger log)
        {
            {
                try
                {
                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                    var blComercio = new UserManager(repositorio, _encriptacionService);

                    await blComercio.EliminarUsuarioPorId(userId);

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



        [Function("ObtenerPerfilUsuarioPorNicknameMysql")]
        public async Task<IActionResult> ObtenerPerfilUsuarioPorNicknameMysql(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/usuarioMysql/{nickname}")] HttpRequest req,
           String nickname,
           ILogger log)
        {
            {
                try
                {
                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);

                    var blComercio = new UserManager(repositorio, _encriptacionService);
                    var usuario = await blComercio.ObtenerPerfilUsuarioPorNickname(nickname);

                    return FunctionBaseHttpMensaje.ResultadoObjeto(usuario);
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


        #region ejemplos Table Storage


        //[Function("CrearUsuarioTs")]
        //public async Task<IActionResult> CrearUsuarioTs([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/usuarioTs")] HttpRequest req, ILogger log)
        //{
        //    {
        //        try
        //        {

        //            var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);

        //            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //            var comercioRequest = JsonSerializer.Deserialize<BE.Usuario.Dto.Usuario>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //            var blComercio = new UserManager(repositorio);
        //            await blComercio.CrearUsuarioTs(comercioRequest);

        //            return FunctionBaseHttpMensaje.ResultadoOk();
        //        }
        //        catch (MensajeException mx)
        //        {
        //            return FunctionBaseHttpMensaje.ResultadoMensaje(mx, "Function.Ose.DWH", true);
        //        }
        //        catch (Exception ex)
        //        {
        //            return await FunctionBaseHttpMensaje.ResultadoErrorAsync(ex, "Function.Ose.DWH");
        //        }
        //    }
        //}


        [Function("ObtenerUsuariosTs")]
        public async Task<IActionResult> ObtenerUsuariosTs([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/usuarioTs")] HttpRequest req, ILogger log)
        {
            {
                try
                {

                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);

                    var blComercio = new UserManager(repositorio, _encriptacionService);
                    var usuarios = await blComercio.ObtenerUsuarioTS("613", "20250601");

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

        [Function("ActualizarContrasenaUsuario")]
        public async Task<IActionResult> ActualizarContrasenaUsuario(
    [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v1/usuario/contrasena")] HttpRequest req,
    ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var request = JsonSerializer.Deserialize<ActualizarContrasenaRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                var userManager = new UserManager(repositorio, _encriptacionService);

                await userManager.ActualizarContrasena(request);

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




        [Function("SubirImagenUsuarioTs")]
        public async Task<IActionResult> SubirImagenUsuarioTs([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/Imagen/user")] HttpRequest req, ILogger log)
        {
            {
                try
                {
                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);

                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    var comercioRequest = JsonSerializer.Deserialize<BE.Imagenes.Request.Imagen>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    var blComercio = new UserManager(repositorio, _encriptacionService);
                    await blComercio.SubirImagenUsuarioTS(comercioRequest.Nombre, comercioRequest.ImagenBase64);

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


        #endregion

    }
}
