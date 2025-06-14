namespace UPC.SmartLock.BE.Mienbros.Response
{
    public class MienbroInfoTemporalResponse: IMienbroInfoTemporalResponse
    {
        public string MienbroNombre {  get; set; }
        public string Parentesco {  get; set; }
        public int Edad {  get; set; }
        public string FotoPerfil {  get; set; }
        public int Estatus {  get; set; }
        public string HogarNombre {  get; set; }
        public string TipoHogar {  get; set; }
    }
}
