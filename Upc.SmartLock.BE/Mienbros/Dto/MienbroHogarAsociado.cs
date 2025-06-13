namespace UPC.SmartLock.BE.Mienbros.Dto
{
    public class MienbroHogarAsociado : IMienbroHogarAsociado
    {
        public string Id { get; set; }
        public string HogarId { get; set; }
        public string MienbroId { get; set; }
        public bool Estatus { get; set; }
    }
}
