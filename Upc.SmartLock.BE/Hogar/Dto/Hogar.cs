namespace UPC.SmartLock.BE.Hogar.Dto
{
    public class Hogar : IHogar
    {
        public int Id { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public int PropietarioId { get; set; }
    }
}
