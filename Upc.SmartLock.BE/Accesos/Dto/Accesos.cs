namespace UPC.SmartLock.BE.Accesos.Dto
{
    public class Accesos: IAccesos
    {
        public string Id { get; set; }
        public string HogarId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Resultado { get; set; }
    }
}
