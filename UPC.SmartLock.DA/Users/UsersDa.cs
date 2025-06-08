using UPC.SmartLock.BE.Hogar.Response;
using UPC.SmartLock.BE.Usuario.Dto;
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
            string query = @$" INSERT INTO {TablasMysql.USUARIO} (nombre, email, contraseña)
                              VALUES ('{request.Nombre}', '{request.Correo}', '{request.Contraseña}'); ";

            Conexion.IniciarConsulta(query);
            await Conexion.EjecutarAsync();
        }

        public async Task<List<IUsuarioResponse>> ObtenerUsuarios()
        {
            var lista = new List<IUsuarioResponse>();
            var sql = @$"select id, nombre, email
                        from {TablasMysql.USUARIO}";
            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                var postId = lector.GetOrdinal("id");
                var posNombre = lector.GetOrdinal("nombre");
                var posCorreo = lector.GetOrdinal("email");

                while (lector.Read())
                {
                    var usuario = new UsuarioResponse();
                    usuario.Id = lector.GetInt32(postId);
                    usuario.Nombre = lector.GetString(posNombre);
                    usuario.Correo = lector.GetString(posCorreo);

                    lista.Add(usuario);
                }
            }

            return lista;
        }


        public async Task<IUsuarioResponse> GetUsuarioPorId(int usuarioId)
        {

            IUsuarioResponse usuario = null;
            var sql = @$"
                SELECT id, nombre, email
                FROM {TablasMysql.USUARIO}
                WHERE id = {usuarioId};";

            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                if (await lector.ReadAsync())
                {

                    var posId = lector.GetOrdinal("id");
                    var posNombre = lector.GetOrdinal("nombre");
                    var posCorreo = lector.GetOrdinal("email");

                    usuario = new UsuarioResponse
                    {
                        Nombre = lector.GetString(posNombre),
                        Correo = lector.GetString(posCorreo),
                    };
                }
            }
            return usuario;
        }
    }
}
