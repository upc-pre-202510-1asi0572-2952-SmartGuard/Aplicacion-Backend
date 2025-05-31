namespace UPC.SmartLock.BE.Usuario.Request
{
    public interface IUsuarioRequest
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }
}
