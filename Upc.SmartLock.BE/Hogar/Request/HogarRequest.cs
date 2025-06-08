namespace UPC.SmartLock.BE.Hogar.Request
{
    public class HogarRequest : IHogarRequest
    {
        public int Id { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public int? PropietarioId { get; set; }
    }
}
