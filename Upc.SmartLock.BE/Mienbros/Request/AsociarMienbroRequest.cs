namespace UPC.SmartLock.BE.Mienbros.Request
{
    public class AsociarMienbroRequest : IAsociarMienbroRequest
    {
        public string HogarId { get ; set ; }
        public string MienbroId { get ; set ; }
        public bool Estatus { get ; set ; }
    }
}
