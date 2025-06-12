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
            string query = @$"
                            INSERT INTO {TablasMysql.USUARIO}
                            (id, nombre, apellido, nickname, telefono, email, contrasenia, ruta_rostros, foto_perfil)
                            VALUES (
                                UNHEX(REPLACE('{request.Id}', '-', '')),
                                '{request.Nombre}',
                                '{request.Apellido}',
                                '{request.Nickname}',
                                {(request.Telefono != null ? $"'{request.Telefono}'" : "NULL")},
                                {(request.Email != null ? $"'{request.Email}'" : "NULL")},
                                '{request.Contrasenia}',
                                '{request.RutaRostros}',
                                {(request.FotoPerfil != null ? $"'{request.FotoPerfil}'" : "NULL")}
                            );
                        ";

            Conexion.IniciarConsulta(query);
            await Conexion.EjecutarAsync();
        }

        //public async Task<List<IUsuarioResponse>> ObtenerUsuarios()
        //{
        //    var lista = new List<IUsuarioResponse>();
        //    var sql = @$"select id, nombre, email
        //                from {TablasMysql.USUARIO}";
        //    Conexion.IniciarConsulta(sql);

        //    using (var lector = await Conexion.EjecutarLectorAsync())
        //    {
        //        var postId = lector.GetOrdinal("id");
        //        var posNombre = lector.GetOrdinal("nombre");
        //        var posCorreo = lector.GetOrdinal("email");

        //        while (lector.Read())
        //        {
        //            var usuario = new UsuarioResponse();
        //            usuario.Id = lector.GetInt32(postId);
        //            usuario.Nombre = lector.GetString(posNombre);
        //            usuario.Correo = lector.GetString(posCorreo);

        //            lista.Add(usuario);
        //        }
        //    }

        //    return lista;
        //}


        public async Task<IUsuarioResponse> BuscarUsuarioXNickname(string nickname)
        {

            IUsuarioResponse usuario = null;
            var sql = @$"
                SELECT *
                FROM {TablasMysql.USUARIO}
                WHERE nickname = '{nickname}';";

            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                if (await lector.ReadAsync())
                {

                    var posId = lector.GetOrdinal("id");
                    var posNombre = lector.GetOrdinal("nombre");
                    var posApellido = lector.GetOrdinal("apellido");
                    var posNickname = lector.GetOrdinal("nickname");
                    var posTelefono = lector.GetOrdinal("telefono");
                    var posEmail = lector.GetOrdinal("email");
                    var posContrasenia = lector.GetOrdinal("contrasenia");
                    var posRutaRostros = lector.GetOrdinal("ruta_rostros");
                    var posFotoPerfil = lector.GetOrdinal("foto_perfil");

                    usuario = new UsuarioResponse
                    {
                        Id = lector.GetGuidString(posId),
                        Nombre = lector.GetString(posNombre),
                        Apellido = lector.GetString(posApellido),
                        Nickname = lector.GetString(posNickname),
                        Telefono = lector.GetString(posTelefono),
                        Email = lector.GetString(posEmail),
                        Contrasenia = lector.GetString(posContrasenia),
                        RutaRostros = lector.GetString(posRutaRostros),
                        FotoPerfil = lector.GetString(posFotoPerfil)
                    };
                }
            }
            return usuario;
        }
    }
}
