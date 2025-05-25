using System.Data.Common;
using System.Text;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public static class ConversorBulk
    {
        public class Configuracion
        {
            private Func<string, Func<DbDataReader, int, string>> _ConversorValor;

            public Func<DbDataReader, int, string> ConversorValor(string nombre)
            {
                return _ConversorValor(nombre);
            }

            public Configuracion(Func<string, Func<DbDataReader, int, string>> conversorValor)
            {
                _ConversorValor = conversorValor;
            }
        }

        public static Encoding ENCODING = Encoding.UTF8;

        public static string ENCODING_CHARSET = "utf8";

        public static string BULK_FECHA_FORMATO = "yyyy-MM-dd";

        public static string BULK_HORA_FORMATO = "HH:mm:ss";

        public const char SEPARADOR_FILA = '~';

        public const char SEPARADOR_COLUMNA = '|';

        public const char REEMPLAZO_COMODIN = '%';

        private static string Codificar(this string value)
        {
            List<char> list = new List<char>(value.ToCharArray());
            int length = value.Length;
            char c = '\0';
            for (int i = 0; i < length; i++)
            {
                switch (list[i])
                {
                    case '|':
                        list[i] = 'c';
                        list.Insert(i, '%');
                        i++;
                        break;
                    case '~':
                        list[i] = 'f';
                        list.Insert(i, '%');
                        i++;
                        break;
                }
            }

            return new string(list.ToArray());
        }

        public static string ConversorLectorDefault(DbDataReader lector, int posicion)
        {
            if (!lector.IsDBNull(posicion))
            {
                return Convert.ToString(lector.GetValue(posicion));
            }

            return string.Empty;
        }

        public static string ConversorLectorCadena(DbDataReader lector, int posicion)
        {
            if (!lector.IsDBNull(posicion))
            {
                return lector.GetString(posicion).Codificar();
            }

            return string.Empty;
        }

        public static string ConversorLectorFechaHora(DbDataReader lector, int posicion)
        {
            if (!lector.IsDBNull(posicion))
            {
                return lector.GetDateTime(posicion).ToString(BULK_FECHA_FORMATO + " " + BULK_HORA_FORMATO);
            }

            return string.Empty;
        }

        public static string ConversorLectorHora(DbDataReader lector, int posicion)
        {
            if (!lector.IsDBNull(posicion))
            {
                return ((TimeSpan)lector.GetValue(posicion)).ToString();
            }

            return string.Empty;
        }

        public static string ConversorLectorFecha(DbDataReader lector, int posicion)
        {
            if (!lector.IsDBNull(posicion))
            {
                return lector.GetDateTime(posicion).ToString(BULK_FECHA_FORMATO);
            }

            return string.Empty;
        }

        public static void SerializarLector(Stream flujo, Configuracion configuracion, DbDataReader lector)
        {
            Func<DbDataReader, int, string>[] array = new Func<DbDataReader, int, string>[lector.FieldCount];
            int num = 0;
            int fieldCount = lector.FieldCount;
            if (!lector.HasRows || fieldCount <= 0)
            {
                return;
            }

            fieldCount = lector.FieldCount - 1;
            using (StreamWriter streamWriter = new StreamWriter(flujo, ENCODING, 1024, leaveOpen: true))
            {
                for (num = 0; num < fieldCount; num++)
                {
                    streamWriter.Write(lector.GetName(num));
                    streamWriter.Write('|');
                }

                streamWriter.Write(lector.GetName(num));
                streamWriter.Write('~');
                for (num = 0; num < fieldCount; num++)
                {
                    array[num] = configuracion.ConversorValor(lector.GetDataTypeName(num));
                    streamWriter.Write(lector.GetDataTypeName(num));
                    streamWriter.Write('|');
                }

                array[num] = configuracion.ConversorValor(lector.GetDataTypeName(num));
                streamWriter.Write(lector.GetDataTypeName(num));
                streamWriter.Write('~');
                while (lector.Read())
                {
                    for (num = 0; num < fieldCount; num++)
                    {
                        streamWriter.Write(array[num](lector, num));
                        streamWriter.Write('|');
                    }

                    streamWriter.Write(array[num](lector, num));
                    streamWriter.Write('~');
                }
            }

            flujo.SetLength(flujo.Length - 1);
            flujo.Position = 0L;
        }

        public static async Task SerializarLectorAsync(Stream flujo, Configuracion configuracion, DbDataReader lector)
        {
            Func<DbDataReader, int, string>[] conversorArray = new Func<DbDataReader, int, string>[lector.FieldCount];
            int m = lector.FieldCount;
            if (!lector.HasRows || m <= 0)
            {
                return;
            }

            m = lector.FieldCount - 1;
            using (StreamWriter escritor = new StreamWriter(flujo, ENCODING, 1024, leaveOpen: true))
            {
                int k;
                for (k = 0; k < m; k++)
                {
                    await escritor.WriteAsync(lector.GetName(k));
                    await escritor.WriteAsync('|');
                }

                await escritor.WriteAsync(lector.GetName(k));
                await escritor.WriteAsync('~');
                for (k = 0; k < m; k++)
                {
                    conversorArray[k] = configuracion.ConversorValor(lector.GetDataTypeName(k));
                    await escritor.WriteAsync(lector.GetDataTypeName(k));
                    await escritor.WriteAsync('|');
                }

                conversorArray[k] = configuracion.ConversorValor(lector.GetDataTypeName(k));
                await escritor.WriteAsync(lector.GetDataTypeName(k));
                await escritor.WriteAsync('~');
                while (lector.Read())
                {
                    for (k = 0; k < m; k++)
                    {
                        await escritor.WriteAsync(conversorArray[k](lector, k));
                        await escritor.WriteAsync('|');
                    }

                    await escritor.WriteAsync(conversorArray[k](lector, k));
                    await escritor.WriteAsync('~');
                }
            }

            flujo.SetLength(flujo.Length - 1);
            flujo.Position = 0L;
        }
    }
}
