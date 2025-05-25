using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.DA
{
    public class BaseDA
    {
        #region Propiedades
        public ConexionMysql Conexion { get; set; }
        #endregion

        #region Metodos Generales
        public object EsNulo(object expresion, object valor)
        {
            return (expresion == DBNull.Value || expresion == null ? valor : expresion);
        }

        protected async ValueTask<bool> ExisteTablaAsync(string nombre)
        {
            Conexion.IniciarConsulta("select count(1) as contador from information_schema.TABLES where TABLE_SCHEMA = database() and TABLE_NAME = ?p_nombre limit 1;");

            Conexion.NuevoParametro("p_nombre", nombre);

            return int.Parse((await Conexion.EjecutarValorAsync<object>()).ToString()) > 0;
        }
        #endregion

        #region Constructores
        public BaseDA(ConexionMysql conexion)
        {
            Conexion = conexion;
        }
        #endregion
    }
}
