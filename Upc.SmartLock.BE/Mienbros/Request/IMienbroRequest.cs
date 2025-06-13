namespace UPC.SmartLock.BE.Mienbros.Request
{
    public interface IMienbroRequest
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Parentesco { get; set; }
        public string Descripcion { get; set; }
        public string FotoPerfil { get; set; }
        public string HogarId { get; set; }
        public string userNickname { get; set; }
    }
}
