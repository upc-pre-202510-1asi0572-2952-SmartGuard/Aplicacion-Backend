using Azure.Storage.Blobs;
using Microsoft.Azure.Cosmos.Table;


namespace UPC.SmartLock.BE.Util.Librarys
{
    public abstract class AlmacenamientoBase : IAlmacenamiento
    {
        public string Llave { get; set; }

        public string Nombre { get; set; }

        internal CloudStorageAccount CuentaStorage { get; set; }

        internal CloudTableClient ClienteTabla { get; set; }

        internal BlobServiceClient ClienteBlob { get; set; }
    }
}
