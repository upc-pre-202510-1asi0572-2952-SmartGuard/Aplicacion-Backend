using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BE.Usuario.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.DA.Users
{
    public class UsersDa : BaseDA
    {
        #region Constructor
        public UsersDa(ConexionMysql conexion) : base(conexion)
        {

        }
        #endregion

        public async Task AgregarNuevoUsuario(IUsuarioRequest request)
        {
            string query = @$" INSERT INTO {TablasMysql.USUARIO} (nombre, correo, contraseña)
                              VALUES ('{request.Nombre}', '{request.Correo}', '{request.Contraseña}'); ";

            Conexion.IniciarConsulta(query);
            await Conexion.EjecutarAsync();
        }

        public async Task<List<IUsuarioResponse>> ObtenerUsuarios()
        {
            var lista = new List<IUsuarioResponse>();
            var sql = @$"select *
                        from {TablasMysql.USUARIO}";
            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                var posNombre = lector.GetOrdinal("nombre");
                var posCorreo = lector.GetOrdinal("correo");
                var posContraseña = lector.GetOrdinal("contraseña");

                while (lector.Read())
                {
                    var usuario = new UsuarioResponse();

                    usuario.Nombre = lector.GetString(posNombre);
                    usuario.Correo = lector.GetString(posCorreo);
                    usuario.Contraseña = lector.GetString(posContraseña);

                    lista.Add(usuario);
                }
            }

            return lista;
        }
    }
}
