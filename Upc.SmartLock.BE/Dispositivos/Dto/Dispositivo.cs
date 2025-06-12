
namespace UPC.SmartLock.BE.Dispositivos.Dto
{
    public class Dispositivo : IDispositivo
    {
        public string Id { get; set; }
        public string HogarId { get; set; }
        public string Serie { get; set; }
        public string Modelo { get; set; }
    }
}
