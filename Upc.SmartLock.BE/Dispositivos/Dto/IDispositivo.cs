namespace UPC.SmartLock.BE.Dispositivos.Dto
{
    public interface IDispositivo
    {
        public string Id { get; set; }
        public string HogarId { get; set; }
        public string Serie { get; set; }
        public string Modelo { get; set; }
        public int Porcentaje {  get; set; }
        public bool Puerta {  get; set; }
        public string Firmware {  get; set; } 
    }
}
