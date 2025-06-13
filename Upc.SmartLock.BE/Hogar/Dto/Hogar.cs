namespace UPC.SmartLock.BE.Hogar.Dto
{
    public class Hogar : IHogar
    {
        public string Id { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public string PropietarioId { get; set; }
        public string ImgUrl { get; set; }
    }
}
