using UPC.SmartLock.BE.Hogar.Dto;
using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.DA.Homes
{
    public class HogarTs : TableStorageAcceso<IHogar, HogarTs.Objeto>
    {
        #region Propiedades
        private IAlmacenamiento _almacenamiento = null;
        protected override string Nombre => "Hogarejemplo";
        #endregion

        #region Metodos
        protected override Objeto CrearObjeto(IHogar valor)
        {
            return new Objeto()
            {
                PartitionKey = valor.Id.ToString(),
                RowKey = DateTime.Now.ToString("yyyyMMdd"),
                Id = valor.Id,
                Nombre = valor.Nombre,
                Direccion = valor.Direccion,
                PropietarioId = valor.PropietarioId,
            };
        }
        public async ValueTask<IHogar> SeleccionarPorIdAsync(string partitionKey, string rowKey)
        {
            var consulta = ConsultaCombinar(
                ConsultaFiltro(nameof(Objeto.PartitionKey), ConsultaEqual, partitionKey),
                ConsultaAnd,
                ConsultaFiltro(nameof(Objeto.RowKey), ConsultaEqual, rowKey));

            return (await ListarPorConsultaAsync(consulta)).FirstOrDefault();
        }
        public async ValueTask<List<IHogar>> ListarPorIdAsync(string partitionKey)
        {
            var consulta = ConsultaFiltro(nameof(Objeto.PartitionKey), ConsultaEqual, partitionKey);

            return (await ListarPorConsultaAsync(consulta)).ToList();
        }
        public async Task SubirLogoComercio(string blobNombre, Stream content)
        {
            var almacenamiento = _almacenamiento.ObtenerClienteBlob();
            var container = almacenamiento.GetBlobContainerClient("fotos");
            await container.UploadBlobAsync(blobNombre, content);
        }
        #endregion

        #region Clases
        public class Objeto : TableStorageBase, IHogar
        {
            public string Id { get; set; }
            public string Nombre { get; set; }
            public string Direccion { get; set; }
            public string PropietarioId { get; set; }
            public string TipoPropiedad { get; set; }
            public int Habitaciones { get; set; }
            public int Baños { get; set; }
            public bool Calefaccion { get; set; }
            public string AbastecimientoAgua { get; set; }
            public string ProveedorInternet { get; set; }
            public string SistemaSeguridad { get; set; }
            public int FuncionesInteligentes { get; set; }
            public string ImgUrl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }
        #endregion

        #region Constructores
        public HogarTs(IAlmacenamiento almacenamiento) : base(almacenamiento)
        {
            _almacenamiento = almacenamiento;
        }
        #endregion

    }
}
