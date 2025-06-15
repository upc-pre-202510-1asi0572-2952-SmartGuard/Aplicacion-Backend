namespace UPC.SmartLock.BE.Usuario.Response
{
    public class UsuarioResponse : IUsuarioResponse
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Nickname { get; set; }
        public string Contrasenia { get; set; }
        public string RutaRostros { get; set; }
        public string Email { get; set; }
        public string? Telefono { get; set; }
        public string? FotoPerfil { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Genero { get; set; }
        public string? Ubicacion { get; set; }
        public string? Ocupacion { get; set; }
        public string? Direccion { get; set; }
    }
}
