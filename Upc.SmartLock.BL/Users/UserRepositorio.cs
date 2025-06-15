using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json.Linq;
using UPC.SmartLock.BE.Hogar.Response;
using UPC.SmartLock.BE.Usuario.Dto;
using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BE.Usuario.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.DA.Homes;
using UPC.SmartLock.DA.Users;

namespace UPC.SmartLock.BL.Users
{
    public class UserRepositorio : IUserRepositorio
    {
        private Repositorio _repositorio;
        public UserRepositorio(Repositorio Repositorio)
        {
            _repositorio = Repositorio;
        }

        public async Task<IUsuarioResponse> BuscarUsuarioXNickname(string nickname)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                var data = new UsersDa(Conexion);
                return await data.BuscarUsuarioXNickname(nickname);
            }
        }

        #region Metodos Mysql

        public async Task InsertarUsuario(IUsuarioRequest value)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                Conexion.IniciarTransaccion();
                try
                {
                    var data = new UsersDa(Conexion);
                    await data.AgregarNuevoUsuario(value);
                    Conexion.EjecutarTransaccion();
                }
                catch (Exception ex)
                {
                    Conexion.CancelarTransaccion();
                    throw new BE.Util.MensajeExceptionExtendido(ex.Message);
                }
            }

        }

        public async Task<IUsuarioResponse> BuscarUsuarioXEmail(string email)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                var data = new UsersDa(Conexion);
                return await data.BuscarUsuarioXEmail(email);
            }
        }




        #endregion


        #region Metodos Table Storage


        public async Task InsertarUsuarioTs(IUsuario value)
        {
            var data = new UsersTs(_repositorio.Almacenamiento);
            await data.InsertarAsync(value);
        }

        public async Task<IUsuario> ObtenerUsuarioTs(string partitionKey, string rowKey)
        {
            var data = new UsersTs(_repositorio.Almacenamiento);
            return await data.SeleccionarPorIdAsync(partitionKey, rowKey);
        }

        public async Task SubirImagen(string blobNombre, Stream content)
        {
            var data = new UsersTs(_repositorio.Almacenamiento);
            await data.SubirLogoComercio(blobNombre, content);
        }

       
        #endregion

    }
}
