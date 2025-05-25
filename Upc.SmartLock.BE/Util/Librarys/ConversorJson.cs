using Utf8Json;
using Utf8Json.Resolvers;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public class ConversorJson
    {
        public static IJsonFormatterResolver Formato = StandardResolver.Default;

        public static byte[] Serializar<T>(T value)
        {
            return Utf8Json.JsonSerializer.Serialize(value, Formato);
        }

        public static void Serializar<T>(Stream stream, T valor)
        {
            Utf8Json.JsonSerializer.Serialize(stream, valor, Formato);
        }

        public static string ObtenerCadena<T>(T valor)
        {
            return Utf8Json.JsonSerializer.ToJsonString(valor, Formato);
        }

        public static T Deserializar<T>(Stream stream)
        {
            return Utf8Json.JsonSerializer.Deserialize<T>(stream, Formato);
        }

        public static T Deserializar<T>(byte[] buffer)
        {
            return Utf8Json.JsonSerializer.Deserialize<T>(buffer, Formato);
        }

        public static T Deserializar<T>(string json)
        {
            return Utf8Json.JsonSerializer.Deserialize<T>(json, Formato);
        }

        public static async Task SerializarAsync<T>(Stream stream, T value)
        {
            await Utf8Json.JsonSerializer.SerializeAsync(stream, value, Formato);
        }

        public static async Task<MemoryStream> SerializarAsync<T>(T valor)
        {
            MemoryStream strean = new MemoryStream();
            await Utf8Json.JsonSerializer.SerializeAsync(strean, valor, Formato);
            return strean;
        }

        public static async Task<T> DeserializarAsync<T>(Stream stream)
        {
            return await Utf8Json.JsonSerializer.DeserializeAsync<T>(stream, Formato);
        }

        public static string ObtenerJsonFormateado(byte[] buffer)
        {
            return Utf8Json.JsonSerializer.PrettyPrint(buffer);
        }
    }
}
