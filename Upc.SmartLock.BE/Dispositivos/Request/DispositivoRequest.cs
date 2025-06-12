namespace UPC.SmartLock.BE.Dispositivos.Request
{
    public class DispositivoRequest : IDispositivoRequest
    {
        public string Id { get; set; }
        public string HogarId { get; set; }
        public string Serie { get; set; }
        public string Modelo { get; set; }
    }
}
