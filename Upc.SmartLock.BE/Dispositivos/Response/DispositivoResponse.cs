namespace UPC.SmartLock.BE.Dispositivos.Response
{
    public class DispositivoResponse : IDispositivoResponse
    {
        public string Id { get; set; }
        public string HogarId { get; set; }
        public string Serie { get; set; }
        public string Modelo { get; set; }
    }
}
