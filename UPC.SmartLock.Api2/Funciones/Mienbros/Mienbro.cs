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

    }
}
