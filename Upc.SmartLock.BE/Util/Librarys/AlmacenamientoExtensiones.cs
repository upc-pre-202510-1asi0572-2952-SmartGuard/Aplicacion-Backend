using Azure.Storage.Blobs;
using Microsoft.Azure.Cosmos.Table;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public static class AlmacenamientoExtensiones
    {
        public static string CADENA_CONEXION_FORMATO = "DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1};EndpointSuffix=core.windows.net";

        internal static CloudStorageAccount ObtenerStorageAccount(this IAlmacenamiento almacenamiento)
        {
            AlmacenamientoBase almacenamientoBase = almacenamiento as AlmacenamientoBase;
            if (almacenamientoBase == null)
            {
                if (almacenamientoBase.CuentaStorage == null)
                {
                    almacenamientoBase.CuentaStorage = new CloudStorageAccount(new StorageCredentials(almacenamiento.Nombre, almacenamiento.Llave), useHttps: true);
                }

                return almacenamientoBase.CuentaStorage;
            }

            return new CloudStorageAccount(new StorageCredentials(almacenamiento.Nombre, almacenamiento.Llave), useHttps: true);
        }

        internal static CloudStorageAccount ObtenerStorageAccount()
        {
            return Almacenamiento.Instancia.ObtenerStorageAccount();
        }

        public static BlobServiceClient ObtenerClienteBlob(this IAlmacenamiento almacenamiento)
        {
            if (almacenamiento is AlmacenamientoBase almacenamientoBase)
            {
                if (almacenamientoBase.ClienteBlob == null)
                {
                    almacenamientoBase.ClienteBlob = new BlobServiceClient(string.Format(CADENA_CONEXION_FORMATO, almacenamiento.Nombre, almacenamiento.Llave));
                }

                return almacenamientoBase.ClienteBlob;
            }

            return new BlobServiceClient(string.Format(CADENA_CONEXION_FORMATO, almacenamiento.Nombre, almacenamiento.Llave));
        }

        public static BlobServiceClient ObtenerClienteBlob()
        {
            return Almacenamiento.Instancia.ObtenerClienteBlob();
        }

        public static CloudTableClient ObtenerClienteTabla(this IAlmacenamiento almacenamiento)
        {
            if (almacenamiento is AlmacenamientoBase almacenamientoBase)
            {
                if (almacenamientoBase.ClienteTabla == null)
                {
                    almacenamientoBase.ClienteTabla = almacenamiento.ObtenerStorageAccount().CreateCloudTableClient();
                }

                return almacenamientoBase.ClienteTabla;
            }

            return almacenamiento.ObtenerStorageAccount().CreateCloudTableClient();
        }

        public static CloudTableClient ObtenerClienteTabla()
        {
            return ObtenerStorageAccount().CreateCloudTableClient();
        }
    }
}
