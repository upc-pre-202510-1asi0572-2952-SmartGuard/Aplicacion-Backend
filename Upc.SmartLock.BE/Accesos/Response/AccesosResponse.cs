namespace UPC.SmartLock.BE.Accesos.Response
{
    public class AccesosResponse: IAccesosResponse
    {
        public string Id { get; set; }
        public string HogarId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Resultado { get; set; }
    }
}
