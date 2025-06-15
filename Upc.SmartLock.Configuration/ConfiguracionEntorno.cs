using Microsoft.Extensions.Configuration;
using UPC.SmartLock.BE.Util.Enum;

namespace UPC.SmartLock.Configuration
{
    public class ConfiguracionEntorno
    {
        #region Campos
        private static IConfiguration configuracion = default(IConfiguration);
        private static string _RUTA_BASE;
        private static string _CADENA_CONEXION;
        private static int? _LOG_NIVEL;
        private static string _CUENTA_ALMACENAMIENTO;


        private static string _ENCRIPTACION_SALTO;
        private static string _ENCRIPTACION_LLAVE;


        #endregion

        #region Getters

        public static string CUENTA_ALMACENAMIENTO
        {
            get
            {
                if (string.IsNullOrEmpty(_CUENTA_ALMACENAMIENTO))
                {
                    _CUENTA_ALMACENAMIENTO = Environment.GetEnvironmentVariable(nameof(CUENTA_ALMACENAMIENTO), EnvironmentVariableTarget.Process)
                    ?? configuracion[nameof(CUENTA_ALMACENAMIENTO)]
                    ?? throw new Exception($"La propiedad {nameof(CUENTA_ALMACENAMIENTO)} no tiene valor");
                }
                return _CUENTA_ALMACENAMIENTO;
            }
        }

        public static string CADENA_CONEXION
        {
            get
            {
                if (string.IsNullOrEmpty(_CADENA_CONEXION))
                {
                    _CADENA_CONEXION = Environment.GetEnvironmentVariable(nameof(CADENA_CONEXION), EnvironmentVariableTarget.Process)
                        ?? configuracion[nameof(CADENA_CONEXION)]
                        ?? throw new Exception($"La propiedad {nameof(CADENA_CONEXION)} no tiene valor");
                }
                return _CADENA_CONEXION;
            }
        }

        private static int LOG_NIVEL
        {
            get
            {
                if (!_LOG_NIVEL.HasValue)
                {
                    _LOG_NIVEL = int.Parse(Environment.GetEnvironmentVariable(nameof(LOG_NIVEL), EnvironmentVariableTarget.Process)
                        ?? configuracion[nameof(LOG_NIVEL)]
                        ?? throw new Exception($"La propiedad {nameof(LOG_NIVEL)} no tiene valor"));
                }
                return _LOG_NIVEL.Value;
            }
        }

        public static string ENCRIPTACION_SALTO
        {
            get
            {
                if (string.IsNullOrEmpty(_ENCRIPTACION_SALTO))
                {
                    _ENCRIPTACION_SALTO = Environment.GetEnvironmentVariable(nameof(ENCRIPTACION_SALTO), EnvironmentVariableTarget.Process)
                        ?? configuracion[nameof(ENCRIPTACION_SALTO)]
                        ?? throw new Exception($"La propiedad {nameof(ENCRIPTACION_SALTO)} no tiene valor");
                }
                return _ENCRIPTACION_SALTO;
            }
        }
        public static string ENCRIPTACION_LLAVE
        {
            get
            {
                if (string.IsNullOrEmpty(_ENCRIPTACION_LLAVE))
                {
                    _ENCRIPTACION_LLAVE = Environment.GetEnvironmentVariable(nameof(ENCRIPTACION_LLAVE), EnvironmentVariableTarget.Process)
                        ?? configuracion[nameof(ENCRIPTACION_LLAVE)]
                        ?? throw new Exception($"La propiedad {nameof(ENCRIPTACION_LLAVE)} no tiene valor");
                }
                return _ENCRIPTACION_LLAVE;
            }
        }



        public static string RUTA_BASE
        {
            get => _RUTA_BASE;
        }

        #endregion

        #region Metodos
        public static void Configurar(IConfiguration _configuracion, EnumTipoProyecto aplicacion, string rutaBase = "")
        {
            _RUTA_BASE = rutaBase;

            //Utf8Json.Resolvers.CompositeResolver.RegisterAndSetAsDefault(new Utf8Json.IJsonFormatter[] {
            //    new Utf8Json.Formatters.DateTimeFormatter("yyyy-MM-ddTHH:mm:ss"),
            //    new Utf8Json.Formatters.NullableDateTimeFormatter("yyyy-MM-ddTHH:mm:ss")
            //},
            //new Utf8Json.IJsonFormatterResolver[] {
            //    Utf8Json.Resolvers.StandardResolver.Default,
            //    Utf8Json.Resolvers.EnumResolver.UnderlyingValue,
            //});

            //Texto.ConversorJson.Formato = Utf8Json.Resolvers.CompositeResolver.Instance;

            //configuracion = _configuracion;

            //Seguridad.AESEncriptacion.Instanciar(null, Encoding.UTF8.GetBytes(ENCRIPTACION_LLAVE));
        }

        //public static void ConfigurarLogger(IAlmacenamiento almacenamiento, EnumTipoProyecto proyecto)
        //{
        //    var logger = new Log.AzureLogger(almacenamiento, "log" + proyecto.ToString().ToLower());
        //    Log.Logger.Nivel = LOG_NIVEL;

        //    Log.Logger.Instancia = logger;
        //}


        #endregion

    }
}
