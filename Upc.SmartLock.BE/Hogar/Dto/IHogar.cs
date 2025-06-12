namespace UPC.SmartLock.BE.Hogar.Dto
{
    public interface IHogar
    {
        public string Id { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public string PropietarioId { get; set; }

 
    }
}
