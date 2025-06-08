using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using UPC.SmartLock.Api.Aplicacion;
using UPC.SmartLock.BE.Hogar.Dto;
using UPC.SmartLock.BE.Hogar.Request;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.BL.Homes;
using UPC.SmartLock.BL.Users;
using UPC.SmartLock.Configuration;

namespace UPC.SmartLock.Api2.Funciones.Hogar
{
    public class Hogar : FunctionBase
    {
        private static RepositorioUPC _repositorioUpc = default(RepositorioUPC);

        public Hogar(RepositorioUPC repositorioUpc)
        {
            _repositorioUpc = repositorioUpc;
        }

        #region ejemplosMysql

        [Function("CrearHogarMysql")]
        public async Task<IActionResult> CrearHogarMysql([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/hogarMysql")] HttpRequest req, ILogger log)
        {
            {
                try
                {
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    var comercioRequest = JsonSerializer.Deserialize<HogarRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                  
                    var blComercio = new HomeManager(repositorio);
                    await blComercio.CrearHogar(comercioRequest);
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

        [Function("ObtenerHogaresPorPropietarioIdMysql")]
        public async Task<IActionResult> ObtenerHogaresPorPropietarioIdMysql(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/hogares/propietario/{propietarioId}")] HttpRequest req, 
            int propietarioId, 
            ILogger log)
        {
            {
                try
                {
                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                    var blComercio = new HomeManager(repositorio);
                    var hogares = await blComercio.ObtenerHogaresPorPropietarioId(propietarioId);

                    return FunctionBaseHttpMensaje.ResultadoObjeto(hogares);
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


        [Function("ObtenerHogarPorIdMysql")]
        public async Task<IActionResult> ObtenerHogarPorIdMysql(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/hogarMysql/{hogarId}")] HttpRequest req, 
            int hogarId, 
            ILogger log)
        {
            {
                try
                {

                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                    var blComercio = new HomeManager(repositorio);
                    var hogar = await blComercio.ObtenerHogarPorId(hogarId);

                    return FunctionBaseHttpMensaje.ResultadoObjeto(hogar);
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


        [Function("ActualizarHogarMysql")]
        public async Task<IActionResult> ActualizarHogarMysql(
           [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v1/hogarMysql/{hogarId}")] HttpRequest req,
           int hogarId,
           ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var hogarRequest = JsonSerializer.Deserialize<HogarRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                var blComercio = new HomeManager(repositorio);

                var hogarExistente = await blComercio.ObtenerHogarPorId(hogarId);
                if (hogarExistente == null)
                    return FunctionBaseHttpMensaje.ResultadoMensaje("404", $"Hogar con Id {hogarId} no encontrado.");

                hogarExistente.Nombre = hogarRequest.Nombre;
                hogarExistente.Direccion = hogarRequest.Direccion;
             

                var hogarUpdateRequest = new HogarRequest
                {
                    Id = hogarExistente.Id,
                    Nombre = hogarExistente.Nombre,
                    Direccion = hogarExistente.Direccion,
                    PropietarioId=hogarExistente.PropietarioId
                };
        
                await blComercio.ActualizarHogar(hogarUpdateRequest);

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

        [Function("ObtenerMiembrosHogar")]
        public async Task<IActionResult> ObtenerMiembrosHogar(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/hogar/{hogarId}/miembros")] HttpRequest req,
    int hogarId,
    ILogger log)
        {
            try
            {
                var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                var blComercio = new HomeManager(repositorio);

                var miembros = await blComercio.ObtenerMiembrosHogar(hogarId);
                return FunctionBaseHttpMensaje.ResultadoObjeto(miembros);
            }
            catch (Exception ex)
            {
                return await FunctionBaseHttpMensaje.ResultadoErrorAsync(ex, "Function.ObtenerMiembrosHogar");
            }
        }


        [Function("AsociarMiembroHogar")]
        public async Task<IActionResult> AsociarMiembroHogar(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/hogar/miembro")] HttpRequest req,
    ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var request = JsonSerializer.Deserialize<AsociarMiembroRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                var manageHome = new HomeManager(repositorio);
                var manageUser = new UserManager(repositorio);

                var hogarExistente = await manageHome.ObtenerHogarPorId(request.HogarId);
                if (hogarExistente == null)
                    return FunctionBaseHttpMensaje.ResultadoMensaje("404", $"Hogar con Id {request.HogarId} no encontrado.");
      
                var usuarioExiste = await manageUser.ObtenerUsuarioPorId(request.UserId);
                    if (usuarioExiste == null)
                        return  FunctionBaseHttpMensaje.ResultadoMensaje("404", $"El usuario con ID {request.UserId} no existe.");

                var usuarioExisteEnHogar = await manageHome.ExisteMiembroEnHogar(request);

                if (usuarioExisteEnHogar)
                    return FunctionBaseHttpMensaje.ResultadoMensaje("400", $"El usuario con ID {request.UserId} ya se encuentra registrado en el hogar {request.HogarId}");


                await manageHome.AsociarMiembroHogar(request);

                return FunctionBaseHttpMensaje.ResultadoOk();
            }
            catch (MensajeException mx)
            {
                return FunctionBaseHttpMensaje.ResultadoMensaje(mx, "Function.AsociarMiembroHogar", true);
            }
            catch (Exception ex)
            {
                return await FunctionBaseHttpMensaje.ResultadoErrorAsync(ex, "Function.AsociarMiembroHogar");
            }
        }


        [Function("EliminarMiembroHogar")]
        public async Task<IActionResult> EliminarMiembroHogar(
    [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/hogar/miembro")] HttpRequest req,
    ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var request = JsonSerializer.Deserialize<AsociarMiembroRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);
                var homeManager = new HomeManager(repositorio);

                var hogarExistente = await homeManager.ObtenerHogarPorId(request.HogarId);
                if (hogarExistente == null)
                    return FunctionBaseHttpMensaje.ResultadoMensaje("404", $"Hogar con Id {request.HogarId} no encontrado.");

                var usuarioExisteEnHogar = await homeManager.ExisteMiembroEnHogar(request);

                if (!usuarioExisteEnHogar)
                    return FunctionBaseHttpMensaje.ResultadoMensaje("404", $"El usuario con Id {request.UserId} no está asociado al hogar {request.HogarId}.");
         

                await homeManager.EliminarMiembroHogar(request);

                return FunctionBaseHttpMensaje.ResultadoOk();
            }
            catch (MensajeException mx)
            {
                return FunctionBaseHttpMensaje.ResultadoMensaje(mx, "Function.EliminarMiembroHogar", true);
            }
            catch (Exception ex)
            {
                return await FunctionBaseHttpMensaje.ResultadoErrorAsync(ex, "Function.EliminarMiembroHogar");
            }
        }




        #endregion


        #region ejemplos Table Storage


        [Function("CrearHogarTs")]
        public async Task<IActionResult> CrearHogarTs([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/hogarTs")] HttpRequest req, ILogger log)
        {
            {
                try
                {

                    var repositorio = new Repositorio(_repositorioUpc.CadenaConexion, _repositorioUpc.Almacenamiento);

                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    var comercioRequest = JsonSerializer.Deserialize<BE.Hogar.Dto.Hogar>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    var blComercio = new HomeManager(repositorio);
                    await blComercio.CrearHogarTs(comercioRequest);

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
