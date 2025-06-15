namespace UPC.SmartLock.BE.Usuario.Request
{
    public interface IActualizarContrasenaRequest
    {
        public string Id { get; set; }
        public string ContrasenaActual { get; set; }
        public string NuevaContrasena { get; set; }
        public string ConfirmacionContrasena { get; set; }

    }
}
