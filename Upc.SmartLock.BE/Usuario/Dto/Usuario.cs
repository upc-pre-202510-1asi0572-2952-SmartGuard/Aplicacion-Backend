namespace UPC.SmartLock.BE.Usuario.Dto
{
    public class Usuario : IUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
    }
}
