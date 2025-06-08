namespace UPC.SmartLock.BE.Hogar.Request
{
    public interface IHogarRequest
    {
        public int Id { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public int? PropietarioId { get; set; }
    }
}
