namespace UPC.SmartLock.BE.Util
{
    public class Repositorio : IAlmacenamiento
    {
        public string CadenaConexion { get; set; }
        public UPC.SmartLock.BE.Util.Librarys.IAlmacenamiento Almacenamiento { get; set; }

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cadenaConexion">Cadena de conexión a un motor SQL</param>
        public Repositorio(string cadenaConexion)
        {
            this.CadenaConexion = cadenaConexion;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="almacenamiento">Instancia de una cuenta de almacenamiento de Azure (No-SQL)</param>
        public Repositorio(UPC.SmartLock.BE.Util.Librarys.IAlmacenamiento almacenamiento)
        {
            this.Almacenamiento = almacenamiento;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cadenaConexion">Cadena de conexión a un motor SQL</param>
        /// <param name="almacenamiento">Instancia de una cuenta de almacenamiento de Azure (No-SQL)</param>
        public Repositorio(string cadenaConexion, UPC.SmartLock.BE.Util.Librarys.IAlmacenamiento almacenamiento)
        {
            this.CadenaConexion = cadenaConexion;
            this.Almacenamiento = almacenamiento;
        }
        #endregion
    }
}
