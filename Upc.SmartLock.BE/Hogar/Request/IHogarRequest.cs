namespace UPC.SmartLock.BE.Hogar.Request
{
    public interface IHogarRequest
    {
        public string Nickname { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public string ImgUrl { get; set; }

    }
}
