namespace UPC.SmartLock.BL.Util
{
    public static class Cadena
    {
        public static (string, string) ObtenerCredencialCuentaAlmacenamiento(this string cadenaConexion)
        {
            var campos = cadenaConexion.Split(';', (char)StringSplitOptions.RemoveEmptyEntries);

            var nombre = campos.Where(x => x.ToLower().IndexOf("accountname") > -1).First()?.Split('=', (char)StringSplitOptions.RemoveEmptyEntries)[1];
            var llave = campos.Where(x => x.ToLower().IndexOf("accountkey=") > -1).First()?.Substring(11);

            return (nombre, llave);
        }
    }
}
