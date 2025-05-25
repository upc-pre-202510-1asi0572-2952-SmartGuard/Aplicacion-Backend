using System.Security.Cryptography;
using System.Text;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public abstract class Logger
    {
        public class Error
        {
            public string Mensaje { get; set; }

            public string Trazabilidad { get; set; }

            public string MensajeInterno { get; set; }

            public string Tipo { get; set; }

            public Error(Exception ex)
            {
                Mensaje = ex.Message;
                Trazabilidad = ex.StackTrace;
                MensajeInterno = ((ex.InnerException == null) ? string.Empty : ex.InnerException.Message);
                Tipo = ex.GetType().FullName;
            }
        }

        public class Traza<T>
        {
            public string Id { get; set; }

            public int Nivel { get; set; }

            public T Valor { get; set; }

            public Traza(string id, int nivel, T valor)
            {
                Id = id;
                Nivel = nivel;
                Valor = valor;
            }
        }

        private static int? _NIVEL;

        private static bool? _HASH;

        private static Logger _INSTANCIA;

        public static bool Hash
        {
            get
            {
                if (!_HASH.HasValue)
                {
                    _HASH = false;
                }

                return _HASH.Value;
            }
            set
            {
                _HASH = value;
            }
        }

        public static int Nivel
        {
            get
            {
                if (!_NIVEL.HasValue)
                {
                    _NIVEL = 0;
                }

                return _NIVEL.Value;
            }
            set
            {
                _NIVEL = value;
            }
        }

        public static Logger Instancia
        {
            get
            {
                if (_INSTANCIA == null)
                {
                    _INSTANCIA = new ArchivoLogger();
                }

                return _INSTANCIA;
            }
            set
            {
                _INSTANCIA = value;
            }
        }

        protected abstract void EscribirInterno<T>(DateTime timestamp, T value, string category, string hash);

        protected abstract ValueTask EscribirInternoAsync<T>(DateTime timestamp, T value, string category, string hash);

        public virtual string Escribir<T>(int level, T value, string category, string hash = null)
        {
            DateTime now = DateTime.Now;
            string text = now.ToString("HHmmssffff");
            if (level >= Nivel)
            {
                EscribirInterno(now, new Traza<T>(text, level, value), category, hash);
            }

            return text;
        }

        public virtual async ValueTask<string> WriteAsync<T>(int level, T value, string category, string hash = null)
        {
            DateTime now = DateTime.Now;
            string id = now.ToString("HHmmssffff");
            if (level >= Nivel)
            {
                await EscribirInternoAsync(now, new Traza<T>(id, level, value), category, hash);
            }

            return id;
        }

        public virtual string Escribir<T>(LoggerLevel level, T value, string category, string hash = null)
        {
            return Escribir((int)level, value, category, hash);
        }

        public virtual async ValueTask<string> EscribirAsync<T>(LoggerLevel level, T value, string category, string hash = null)
        {
            return await WriteAsync((int)level, value, category, hash);
        }

        public virtual string EscribirError(Exception ex, string category, string hash = null)
        {
            return Escribir(LoggerLevel.ERRORES, new Error(ex), category, hash);
        }

        public async Task<string> EscribirErrorAsync(Exception ex, string category, string hash = null)
        {
            return await EscribirAsync(LoggerLevel.ERRORES, new Error(ex), category, hash);
        }

        public virtual string EscribirError(Exception ex, string category)
        {
            Error error = new Error(ex);
            string hash = null;
            if (Hash)
            {
                using SHA256 sHA = SHA256.Create();
                hash = HexEncoding.GetString(sHA.ComputeHash(Encoding.UTF8.GetBytes(error.Trazabilidad)));
            }

            return Escribir(LoggerLevel.ERRORES, error, category, hash);
        }

        public async Task<string> EscribirErrorAsync(Exception ex, string category)
        {
            Error error = new Error(ex);
            string hash = null;
            if (Hash)
            {
                using SHA256 sHA = SHA256.Create();
                hash = HexEncoding.GetString(sHA.ComputeHash(Encoding.UTF8.GetBytes(error.Trazabilidad)));
            }

            return await EscribirAsync(LoggerLevel.ERRORES, new Error(ex), category, hash);
        }
    }
}
