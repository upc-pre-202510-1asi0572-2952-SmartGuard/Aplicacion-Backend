namespace UPC.SmartLock.BE.Mienbros.Response
{
    public class MienbroResponse : IMienbroResponse
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Parentesco { get; set; }
        public string Descripcion { get; set; }
        public string FotoPerfil { get; set; }
        public string HogarId { get; set; }
        public string UserId { get; set; }
    }
}
