using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UPC.SmartLock.BE.Util.Enum;
using UPC.SmartLock.BL.Util;
using UPC.SmartLock.Configuration;

[assembly: FunctionsStartup(typeof(UPC.SmartLock.Api2.Aplicacion.Startup))]
namespace UPC.SmartLock.Api2.Aplicacion
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var aplicacion = EnumTipoProyecto.SmartLock;

            var configuration = BuildConfiguration(builder.GetContext().ApplicationRootPath);
            ConfiguracionEntorno.Configurar(configuration, aplicacion, builder.GetContext().ApplicationRootPath);

            var repositorioUpc = new RepositorioUPC(ConfiguracionEntorno.CADENA_CONEXION, new CuentaAlmacenamientoExtendido(ConfiguracionEntorno.CUENTA_ALMACENAMIENTO));
            builder.Services.AddScoped(_ => repositorioUpc);


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
