namespace UPC.SmartLock.BE.Accesos.Request
{
    public class IccesosRequest : IAccesosRequest
    {
        public string Id { get; set; }
        public string HogarId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Resultado { get; set; }
    }
}
