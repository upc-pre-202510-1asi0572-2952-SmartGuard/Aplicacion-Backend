namespace UPC.SmartLock.BE.Hogar.Response
{
    public interface IHogarResponse
    {
        public int Id { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public int PropietarioId { get; set; }
    }
}
