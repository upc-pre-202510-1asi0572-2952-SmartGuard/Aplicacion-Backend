using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UPC.SmartLock.Configuration;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

var repositorioUpc = new RepositorioUPC(ConfiguracionEntorno.CADENA_CONEXION);
builder.Services.AddSingleton(_ => repositorioUpc);

builder.Build().Run();
