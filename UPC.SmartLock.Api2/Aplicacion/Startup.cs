using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using UPC.SmartLock.BE.Util.Enum;

[assembly: FunctionsStartup(typeof(UPC.SmartLock.Api.Aplicacion.Startup))]
namespace UPC.SmartLock.Api.Aplicacion
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var aplicacion = EnumTipoProyecto.SmartLock;

            var configuration = BuildConfiguration(builder.GetContext().ApplicationRootPath);
            //ConfiguracionEntorno.Configurar(configuration, aplicacion, builder.GetContext().ApplicationRootPath);

            //var repositorioPseCloud = new RepositorioPseCloud("", new CuentaAlmacenamientoExtendido(ConfiguracionEntorno.CUENTA_ALMACENAMIENTO_PSE_CLOUD));
            //builder.Services.AddSingleton(_ => repositorioPseCloud);

            //ConfiguracionEntorno.ConfigurarLogger(repositorioPseCloud.Almacenamiento, aplicacion);
            //builder.Services.AddSingleton(_ => repositorioPseCloud);


            //Logger.Instancia.Escribir(Log.LoggerLevel.INFORMACION, (object)new { Mensaje = "CargaInicial" }, "Startup");

        }

        private IConfiguration BuildConfiguration(string applicationRootPath)
        {
            var config =
                new ConfigurationBuilder()
                    .SetBasePath(applicationRootPath)
#if DEBUG
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
#endif
                    .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

            return config;
        }
    }
}
