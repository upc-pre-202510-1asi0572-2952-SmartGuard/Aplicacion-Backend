using System.Data.Common;
using System.Data;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public interface IConexionBD : IDisposable
    {
        void CargarParametros();

        void Iniciar(string texto, int tipo);

        void IniciarConsulta(string consulta);

        void IniciarProcedimiento(string nombre);

        void NuevoParametro(string nombre, object valor);

        void NuevoParametro(string nombre, DbType tipo);

        T ObtenerParametro<T>(string nombre);

        T ObtenerParametro<T>(int indice);

        int Ejecutar();

        object EjecutarValor();

        T EjecutarValor<T>();

        object EjecutarValor(int indice);

        T EjecutarValor<T>(int indice);

        object EjecutarValor(string nombre);

        T EjecutarValor<T>(string nombre);

        DbDataReader EjecutarLector();

        void EjecutarCsv(Stream flujo, string nombre);

        Task<int> EjecutarAsync();

        Task<object> EjecutarValorAsync();

        Task<T> EjecutarValorAsync<T>();

        Task<object> EjecutarValorAsync(int indice);

        Task<T> EjecutarValorAsync<T>(int indice);

        Task<object> EjecutarValorAsync(string nombre);

        Task<T> EjecutarValorAsync<T>(string nombre);

        Task<DbDataReader> EjecutarLectorAsync();

        Task EjecutarCsvAsync(Stream flujo, string nombre);

        void IniciarTransaccion();

        void EjecutarTransaccion();

        void CancelarTransaccion();

        ValueTask IniciarTransaccionAsync();

        Task EjecutarTransaccionAsync();

        Task CancelarTransaccionAsync();
    }
}
