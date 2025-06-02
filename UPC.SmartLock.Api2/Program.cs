using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UPC.SmartLock.BL.Util;
using UPC.SmartLock.Configuration;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

var repositorioUpc = new RepositorioUPC(ConfiguracionEntorno.CADENA_CONEXION, new CuentaAlmacenamientoExtendido(ConfiguracionEntorno.CUENTA_ALMACENAMIENTO));
builder.Services.AddSingleton(_ => repositorioUpc);

builder.Build().Run();
