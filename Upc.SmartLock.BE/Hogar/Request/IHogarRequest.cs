namespace UPC.SmartLock.BE.Hogar.Request
{
    public interface IHogarRequest
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Nickname { get; set; }
        public string ImgUrl { get; set; }
        public string TipoPropiedad { get; set; }
        public int Habitaciones { get; set; }
        public int Baños { get; set; }
        public bool Calefaccion { get; set; }
        public string AbastecimientoAgua { get; set; }
        public string ProveedorInternet { get; set; }
        public string SistemaSeguridad { get; set; }
        public int FuncionesInteligentes { get; set; }

    }
}
