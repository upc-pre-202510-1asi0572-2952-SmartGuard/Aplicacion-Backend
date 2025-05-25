using System.Reflection;
using System.Text;

namespace UPC.SmartLock.BE.Util.Librarys
{
    internal class ArchivoLogger : Logger
    {
        private static string _NOMBRE_APLICACION;

        private static string _RUTA_CARPETA;

        private static ReaderWriterLock _LOCK;

        public static string NombreAplicacion
        {
            get
            {
                if (string.IsNullOrEmpty(_NOMBRE_APLICACION))
                {
                    _NOMBRE_APLICACION = Assembly.GetExecutingAssembly().FullName;
                }

                return _NOMBRE_APLICACION;
            }
            set
            {
                _NOMBRE_APLICACION = value;
            }
        }

        public static string RutaFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_RUTA_CARPETA))
                {
                    _RUTA_CARPETA = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                }

                return _RUTA_CARPETA;
            }
            set
            {
                _RUTA_CARPETA = value;
            }
        }

        public static ArchivoLogger CrearInstancia()
        {
            _LOCK = new ReaderWriterLock();
            if (!Directory.Exists(RutaFolder))
            {
                Directory.CreateDirectory(RutaFolder);
            }

            return new ArchivoLogger();
        }

        protected override void EscribirInterno<T>(DateTime timestamp, T value, string category, string hash)
        {
            string path = Path.Combine(RutaFolder, NombreAplicacion + "." + timestamp.ToString("dd.MM.yy") + "." + (category ?? "Application") + ".json");
            try
            {
                _LOCK.AcquireWriterLock(TimeSpan.FromMilliseconds(10.0));
                using (FileStream stream = File.Open(path, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen: true);
                    streamWriter.WriteLine("");
                    streamWriter.WriteLine("=".PadLeft(49, '=') + timestamp.ToString("HHmmssffff") + "=".PadLeft(49, '='));
                    if (!string.IsNullOrEmpty(hash))
                    {
                        streamWriter.WriteLine("=".PadLeft(22, '=') + hash + "=".PadLeft(22, '='));
                    }

                    streamWriter.WriteLine(ConversorJson.ObtenerJsonFormateado(ConversorJson.Serializar(value)));
                    if (!string.IsNullOrEmpty(hash))
                    {
                        streamWriter.WriteLine("=".PadLeft(22, '=') + hash + "=".PadLeft(22, '='));
                    }

                    streamWriter.WriteLine("");
                    streamWriter.WriteLine("=".PadLeft(49, '=') + timestamp.ToString("HHmmssffff") + "=".PadLeft(49, '='));
                }

                _LOCK.ReleaseWriterLock();
            }
            catch
            {
            }
        }

        protected override async ValueTask EscribirInternoAsync<T>(DateTime timestamp, T value, string category, string hash)
        {
            string path = Path.Combine(RutaFolder, NombreAplicacion + "." + timestamp.ToString("dd.MM.yy") + "." + (category ?? "Application") + ".json");
            try
            {
                _LOCK.AcquireWriterLock(TimeSpan.FromMilliseconds(10.0));
                using (FileStream stream = File.Open(path, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen: true);
                    await writer.WriteLineAsync("");
                    await writer.WriteLineAsync("=".PadLeft(49, '=') + timestamp.ToString("HHmmssffff") + "=".PadLeft(49, '='));
                    if (!string.IsNullOrEmpty(hash))
                    {
                        await writer.WriteLineAsync("=".PadLeft(22, '=') + hash + "=".PadLeft(22, '='));
                    }

                    await writer.WriteLineAsync(ConversorJson.ObtenerJsonFormateado(ConversorJson.Serializar(value)));
                    if (!string.IsNullOrEmpty(hash))
                    {
                        await writer.WriteLineAsync("=".PadLeft(22, '=') + hash + "=".PadLeft(22, '='));
                    }

                    await writer.WriteLineAsync("");
                    await writer.WriteLineAsync("=".PadLeft(49, '=') + timestamp.ToString("HHmmssffff") + "=".PadLeft(49, '='));
                }

                _LOCK.ReleaseWriterLock();
            }
            catch
            {
            }
        }
    }
}
