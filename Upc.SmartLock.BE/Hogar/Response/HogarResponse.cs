namespace UPC.SmartLock.BE.Hogar.Response
{
    public class HogarResponse : IHogarResponse
    {
        public int Id { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public int PropietarioId { get; set; }
    }
}
