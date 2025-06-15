namespace UPC.SmartLock.BL.Util.Interface
{
    public interface IAESEncriptacion
    {
        string ObtenerCadena(byte[] buffer);
        byte[] ObtenerBuffer(string cadena);
        string EncriptarCadena(string cadena);
        string DesencriptarCadena(string cadena);
    }
}
