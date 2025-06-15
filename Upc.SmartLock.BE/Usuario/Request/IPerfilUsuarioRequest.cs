namespace UPC.SmartLock.BE.Usuario.Request
{
    public interface IPerfilUsuarioRequest
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string Ubicacion { get; set; }
        public string Ocupacion { get; set; }
        public string Direccion { get; set; }
    }
}
