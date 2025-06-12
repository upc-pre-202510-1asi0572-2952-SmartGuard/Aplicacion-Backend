namespace UPC.SmartLock.BE.Usuario.Dto
{
    public class Usuario : IUsuario
    {
        public string Id { get; }
        public string Nombre { get; }
        public string Apellido { get; }
        public string Nickname { get; }
        public string? Telefono { get; }
        public string? Email { get; }
        public string Contrasenia { get; }
        public string RutaRostros { get; }
        public string? FotoPerfil { get; }
    }
}
