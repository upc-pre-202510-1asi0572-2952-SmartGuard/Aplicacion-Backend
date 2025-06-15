using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UPC.SmartLock.BL.Util;
using UPC.SmartLock.BL.Util.Interface;
using UPC.SmartLock.Configuration;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

var repositorioUpc = new RepositorioUPC(ConfiguracionEntorno.CADENA_CONEXION, new CuentaAlmacenamientoExtendido(ConfiguracionEntorno.CUENTA_ALMACENAMIENTO));

builder.Services.AddSingleton(_ => repositorioUpc);

builder.Services.AddSingleton<IAESEncriptacion>(x =>
{
    var bufferSalto = System.Text.Encoding.UTF8.GetBytes(ConfiguracionEntorno.ENCRIPTACION_SALTO);
    var bufferLlave = System.Text.Encoding.UTF8.GetBytes(ConfiguracionEntorno.ENCRIPTACION_LLAVE);

    return new AESEncriptacionExtension(bufferSalto, bufferLlave);
});

builder.Build().Run();
