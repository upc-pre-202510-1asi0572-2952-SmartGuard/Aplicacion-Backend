using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UPC.SmartLock.Api.Aplicacion;
using UPC.SmartLock.BE.Hogar.Request;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BL.Homes;
using UPC.SmartLock.Configuration;
using UPC.SmartLock.BL.Miembros;
using UPC.SmartLock.BE.Mienbros.Request;

namespace UPC.SmartLock.Api2.Funciones.Mienbros
{
    public class Mienbro: FunctionBase
    {
        private static RepositorioUPC _repositorioUpc = default(RepositorioUPC);

        public Mienbro(RepositorioUPC repositorioUpc)
        {
            _repositorioUpc = repositorioUpc;
        }

        [Function("AgregarNuevoMienbro")]
        public async Task<IActionResult> AgregarNuevoMienbro([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/mienbros")] HttpRequest req, ILogger log)
        {
            {
                try
                {
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    var request = JsonSerializer.Deserialize<MienbroRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);

                    var blComercio = new MienbroManager(repositorio);
                    await blComercio.CrearNuevoMienbro(request);
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

        [Function("AsociarMienbroAHogar")]
        public async Task<IActionResult> AsociarMienbroAHogar([HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "v1/asociar_mienbro")] HttpRequest req, ILogger log)
        {
            {
                try
                {
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    var request = JsonSerializer.Deserialize<AsociarMienbroRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);

                    var blComercio = new MienbroManager(repositorio);
                    await blComercio.AsociarMienbroAHogar(request);
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


        [Function("ObtenerMiembrosPorPropietarioNicknameMysql")]
        public async Task<IActionResult> ObtenerMiembrosPorPropietarioNicknameMysql(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/miembros/propietario/{nickname}")] HttpRequest req,
           string nickname,
           ILogger log)
        {
            {
                try
                {
                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                    var blComercio = new MienbroManager(repositorio);
                    var miembros = await blComercio.ObtenerMiembrosPorPropietarioNickname(nickname);
                    return FunctionBaseHttpMensaje.ResultadoObjeto(miembros);
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


        [Function("ActualizarMiembroMysql")]
        public async Task<IActionResult> ActualizarMiembroMysql(
   [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v1/miembroMysql")] HttpRequest req,
   ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var miembroRequest = JsonSerializer.Deserialize<MienbroRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);

                var blComercio = new MienbroManager(repositorio);
                await blComercio.ActualizarMiembro(miembroRequest);
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


        [Function("ObtenerMiembroPorIdMysql")]
        public async Task<IActionResult> ObtenerMiembroPorIdMysql(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/miembroMysql/{miembroId}")] HttpRequest req,
    String miembroId,
    ILogger log)
        {
            {
                try
                {

                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                    var blComercio = new MienbroManager(repositorio);
                    var miembro = await blComercio.ObtenerMiembroPorId(miembroId);

                    return FunctionBaseHttpMensaje.ResultadoObjeto(miembro);
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



        [Function("EliminarMiembroPorIdMysql")]
        public async Task<IActionResult> EliminarMiembroPorIdMysql(
         [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/miembroMysql/{miembroId}")] HttpRequest req,
         string miembroId,
         ILogger log)
        {
            {
                try
                {
                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                    var blComercio = new MienbroManager(repositorio);

                    await blComercio.EliminarMiembroPorId(miembroId);

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

    }
}
