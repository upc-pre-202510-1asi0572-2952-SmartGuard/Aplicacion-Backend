namespace UPC.SmartLock.BE.Usuario.Response
{
    public class UsuarioResponse : IUsuarioResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
    }
}
