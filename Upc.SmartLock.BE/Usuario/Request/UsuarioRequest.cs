namespace UPC.SmartLock.BE.Usuario.Request
{
    public class UsuarioRequest : IUsuarioRequest
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Nickname { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string Contrasenia { get; set; }
        public string RutaRostros { get; set; }
        public string? FotoPerfil { get; set; }
    }
}
