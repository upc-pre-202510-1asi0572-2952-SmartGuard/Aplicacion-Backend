using UPC.SmartLock.BE.Util;

namespace UPC.SmartLock.Configuration
{
    public class RepositorioUPC : Repositorio
    {
        public RepositorioUPC(string cadenaConexion) : base(cadenaConexion)
        {
        }
        public RepositorioUPC(UPC.SmartLock.BE.Util.Librarys.IAlmacenamiento almacenamiento) : base(almacenamiento)
        {
        }
        public RepositorioUPC(string cadenaConexion, UPC.SmartLock.BE.Util.Librarys.IAlmacenamiento almacenamiento) : base(almacenamiento)
        {
        }
    }
}
