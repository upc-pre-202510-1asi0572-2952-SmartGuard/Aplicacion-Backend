namespace UPC.SmartLock.BE.Dispositivos.Response
{
    public class DispositivoResponse : IDispositivoResponse
    {
        public string Id { get; set; }
        public string Modelo { get; set; }
        public int PorcentajeBateria { get; set; }
        public bool Puerta { get; set; }
        public string Firmware { get; set; }
        public string HogarNombre { get; set; }
    }
}
