using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.BL.Util
{
    public class CuentaAlmacenamientoExtendido : AlmacenamientoBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cadenaConexion">Cadena de conexión de la cuenta de almacenamiento de Azure</param>
        public CuentaAlmacenamientoExtendido(string _cadenaConexion)
        {
            var tuple = _cadenaConexion.ObtenerCredencialCuentaAlmacenamiento();
            Nombre = tuple.Item1;
            Llave = tuple.Item2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombre">Nombre de la cuenta de almacenamiento</param>
        /// <param name="llave">Llave de la cuenta de almacenamiento</param>
        public CuentaAlmacenamientoExtendido(string nombre, string llave)
        {
            Nombre = nombre;
            Llave = llave;
        }
    }
}
