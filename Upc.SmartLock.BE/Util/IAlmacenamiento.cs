namespace UPC.SmartLock.BE.Util
{
    public interface IAlmacenamiento
    {
        /// <summary>
        /// Cadena de conexión hacia un motor SQL
        /// </summary>
        string CadenaConexion { get; set; }
        /// <summary>
        /// Instancia de una cuenta de almacenamiento de Azure
        /// </summary>
        UPC.SmartLock.BE.Util.Librarys.IAlmacenamiento Almacenamiento { get; set; }
    }
}
