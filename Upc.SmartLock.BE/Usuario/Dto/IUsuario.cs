namespace UPC.SmartLock.BE.Usuario.Dto
{
    public interface IUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
    }
}
