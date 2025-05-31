using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BE.Usuario.Response;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.BE.Util;
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

        public async Task<List<IUsuarioResponse>> GetUsuarios()
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                var data = new UsersDa(Conexion);
                return await data.ObtenerUsuarios();
            }
        }

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
    }
}
