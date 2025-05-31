namespace UPC.SmartLock.BE.Usuario.Response
{
    public interface IUsuarioResponse
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }
}
