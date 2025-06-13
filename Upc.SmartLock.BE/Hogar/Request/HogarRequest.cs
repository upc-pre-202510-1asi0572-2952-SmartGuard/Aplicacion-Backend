namespace UPC.SmartLock.BE.Hogar.Request
{
    public class HogarRequest : IHogarRequest
    {
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public string Nickname { get; set; }
        public string ImgUrl { get; set; }
    }
}
