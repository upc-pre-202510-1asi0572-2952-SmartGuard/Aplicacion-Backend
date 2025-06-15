namespace UPC.SmartLock.BE.Usuario.Dto
{
    public class Usuario : IUsuario
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Contrasenia { get; set; }
        public string RutaRostros { get; set; }
    }
}
