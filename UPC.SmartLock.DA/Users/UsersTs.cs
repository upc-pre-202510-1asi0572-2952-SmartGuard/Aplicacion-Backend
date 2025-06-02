using UPC.SmartLock.BE.Usuario.Dto;
using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.DA.Users
{
    public class UsersTs : TableStorageAcceso<IUsuario, UsersTs.Objeto>
    {
        #region Propiedades
        private IAlmacenamiento _almacenamiento = null;
        protected override string Nombre => "usuarioejemplo";
        #endregion

        #region Metodos
        protected override Objeto CrearObjeto(IUsuario valor)
        {
            return new Objeto()
            {
                PartitionKey = valor.Id.ToString(),
                RowKey = DateTime.Now.ToString("yyyyMMdd"),
                Id = valor.Id,
                Nombre = valor.Nombre,
                Correo= valor.Correo,
                Contrasenia = valor.Contrasenia,    
            };
        }
        public async ValueTask<IUsuario> SeleccionarPorIdAsync(string partitionKey, string rowKey)
        {
            var consulta = ConsultaCombinar(
                ConsultaFiltro(nameof(Objeto.PartitionKey), ConsultaEqual, partitionKey),
                ConsultaAnd,
                ConsultaFiltro(nameof(Objeto.RowKey), ConsultaEqual, rowKey));

            return (await ListarPorConsultaAsync(consulta)).FirstOrDefault();
        }
        public async ValueTask<List<IUsuario>> ListarPorIdAsync(string partitionKey)
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
        public class Objeto : TableStorageBase, IUsuario
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Correo { get; set; }
            public string Contrasenia { get; set; }
        }
        #endregion

        #region Constructores
        public UsersTs(IAlmacenamiento almacenamiento) : base(almacenamiento)
        {
            _almacenamiento = almacenamiento;
        }
        #endregion

    }
}
