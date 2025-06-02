namespace UPC.SmartLock.BE.Imagenes.Request
{
    public class Imagen : IImagen
    {
        public string Nombre { get; set; }
        public string ImagenBase64 { get; set; }
    }
}
