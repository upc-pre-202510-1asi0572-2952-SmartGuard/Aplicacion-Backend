using UPC.SmartLock.BE.Hogar.Dto;
using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BE.Usuario.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;
using static Mysqlx.Crud.Order.Types;

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
(
    id, nombre, apellido, nickname, contrasenia, ruta_rostros,
    email, telefono, foto_perfil, fecha_nacimiento,
    genero, ubicacion, ocupacion, direccion
)
VALUES (
    UNHEX(REPLACE('{request.Id}', '-', '')),
    '{request.Nombre}',
    '{request.Apellido}',
    '{request.Nickname}',
    '{request.Contrasenia}',
    '{request.RutaRostros}',
    '{request.Email}',
    {(request.Telefono != null ? $"'{request.Telefono}'" : "NULL")},
    {(request.FotoPerfil != null ? $"'{request.FotoPerfil}'" : "NULL")},
    {(request.FechaNacimiento != null ? $"'{request.FechaNacimiento:yyyy-MM-dd}'" : "NULL")},
    {(request.Genero != null ? $"'{request.Genero}'" : "NULL")},
    {(request.Ubicacion != null ? $"'{request.Ubicacion}'" : "NULL")},
    {(request.Ocupacion != null ? $"'{request.Ocupacion}'" : "NULL")},
    {(request.Direccion != null ? $"'{request.Direccion}'" : "NULL")}
);
";
            Conexion.IniciarConsulta(query);
            await Conexion.EjecutarAsync();
        }

        public async Task EliminarUsuarioPorId(string usuarioId)
        {
            var sql = @$"
        DELETE FROM {TablasMysql.USUARIO}
        WHERE id = UNHEX(REPLACE('{usuarioId}', '-', ''));";

            Conexion.IniciarConsulta(sql);
            await Conexion.EjecutarAsync();
        }




        public async Task<IUsuarioResponse> BuscarUsuarioXEmail(string email)
        {

            IUsuarioResponse usuario = null;
            var sql = @$"
                SELECT *
                FROM {TablasMysql.USUARIO}
                WHERE email = '{email}'";

            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                if (await lector.ReadAsync())
                {


                    var posId = lector.GetOrdinal("id");
                    var posNombre = lector.GetOrdinal("nombre");
                    var posApellido = lector.GetOrdinal("apellido");
                    var posNickname = lector.GetOrdinal("nickname");
                    var posContrasenia = lector.GetOrdinal("contrasenia");
                    var posRutaRostros = lector.GetOrdinal("ruta_rostros");
                    var posEmail = lector.GetOrdinal("email");
                    var posTelefono = lector.GetOrdinal("telefono");
                    var posFotoPerfil = lector.GetOrdinal("foto_perfil");
                    var posFechaNacimiento = lector.GetOrdinal("fecha_nacimiento");
                    var posGenero = lector.GetOrdinal("genero");
                    var posUbicacion = lector.GetOrdinal("ubicacion");
                    var posOcupacion = lector.GetOrdinal("ocupacion");
                    var posDireccion = lector.GetOrdinal("direccion");


                    usuario = new UsuarioResponse
                    {
                        Id = lector.GetGuidString(posId),
                        Nombre = lector.GetString(posNombre),
                        Apellido = lector.GetString(posApellido),
                        Nickname = lector.GetString(posNickname),
                        Contrasenia = lector.GetString(posContrasenia),
                        RutaRostros = lector.GetString(posRutaRostros),
                        Email = lector.GetString(posEmail),
                        Telefono = lector.IsDBNull(posTelefono) ? null : lector.GetString(posTelefono),
                        FotoPerfil = lector.IsDBNull(posFotoPerfil) ? null : lector.GetString(posFotoPerfil),
                        FechaNacimiento = lector.IsDBNull(posFechaNacimiento) ? (DateTime?)null : lector.GetDateTime(posFechaNacimiento),
                        Genero = lector.IsDBNull(posGenero) ? null : lector.GetString(posGenero),
                        Ubicacion = lector.IsDBNull(posUbicacion) ? null : lector.GetString(posUbicacion),
                        Ocupacion = lector.IsDBNull(posOcupacion) ? null : lector.GetString(posOcupacion),
                        Direccion = lector.IsDBNull(posDireccion) ? null : lector.GetString(posDireccion)

                    };
                }
            }
            return usuario;
        }


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
                    var posContrasenia = lector.GetOrdinal("contrasenia");
                    var posRutaRostros = lector.GetOrdinal("ruta_rostros");
                    var posEmail = lector.GetOrdinal("email");
                    var posTelefono = lector.GetOrdinal("telefono");
                    var posFotoPerfil = lector.GetOrdinal("foto_perfil");
                    var posFechaNacimiento = lector.GetOrdinal("fecha_nacimiento");
                    var posGenero = lector.GetOrdinal("genero");
                    var posUbicacion = lector.GetOrdinal("ubicacion");
                    var posOcupacion = lector.GetOrdinal("ocupacion");
                    var posDireccion = lector.GetOrdinal("direccion");

                    usuario = new UsuarioResponse
                    {
                        Id = lector.GetGuidString(posId),
                        Nombre = lector.GetString(posNombre),
                        Apellido = lector.GetString(posApellido),
                        Nickname = lector.GetString(posNickname),
                        Contrasenia = lector.GetString(posContrasenia),
                        RutaRostros = lector.GetString(posRutaRostros),
                        Email = lector.GetString(posEmail),
                        Telefono = lector.IsDBNull(posTelefono) ? null : lector.GetString(posTelefono),
                        FotoPerfil = lector.IsDBNull(posFotoPerfil) ? null : lector.GetString(posFotoPerfil),
                        FechaNacimiento = lector.IsDBNull(posFechaNacimiento) ? (DateTime?)null : lector.GetDateTime(posFechaNacimiento),
                        Genero = lector.IsDBNull(posGenero) ? null : lector.GetString(posGenero),
                        Ubicacion = lector.IsDBNull(posUbicacion) ? null : lector.GetString(posUbicacion),
                        Ocupacion = lector.IsDBNull(posOcupacion) ? null : lector.GetString(posOcupacion),
                        Direccion = lector.IsDBNull(posDireccion) ? null : lector.GetString(posDireccion)
                    };
                }
            }
            return usuario;
        }


        public async Task<IUsuarioResponse> BuscarUsuarioXId(string userId)
        {

            IUsuarioResponse usuario = null;
            var sql = @$"
            SELECT *
            FROM {TablasMysql.USUARIO}
           WHERE id = UNHEX(REPLACE('{userId}', '-', ''));";

            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                if (await lector.ReadAsync())
                {

                    var posId = lector.GetOrdinal("id");
                    var posNombre = lector.GetOrdinal("nombre");
                    var posApellido = lector.GetOrdinal("apellido");
                    var posNickname = lector.GetOrdinal("nickname");
                    var posContrasenia = lector.GetOrdinal("contrasenia");
                    var posRutaRostros = lector.GetOrdinal("ruta_rostros");
                    var posEmail = lector.GetOrdinal("email");
                    var posTelefono = lector.GetOrdinal("telefono");
                    var posFotoPerfil = lector.GetOrdinal("foto_perfil");
                    var posFechaNacimiento = lector.GetOrdinal("fecha_nacimiento");
                    var posGenero = lector.GetOrdinal("genero");
                    var posUbicacion = lector.GetOrdinal("ubicacion");
                    var posOcupacion = lector.GetOrdinal("ocupacion");
                    var posDireccion = lector.GetOrdinal("direccion");

                    usuario = new UsuarioResponse
                    {
                        Id = lector.GetGuidString(posId),
                        Nombre = lector.GetString(posNombre),
                        Apellido = lector.GetString(posApellido),
                        Nickname = lector.GetString(posNickname),
                        Contrasenia = lector.GetString(posContrasenia),
                        RutaRostros = lector.GetString(posRutaRostros),
                        Email = lector.GetString(posEmail),
                        Telefono = lector.IsDBNull(posTelefono) ? null : lector.GetString(posTelefono),
                        FotoPerfil = lector.IsDBNull(posFotoPerfil) ? null : lector.GetString(posFotoPerfil),
                        FechaNacimiento = lector.IsDBNull(posFechaNacimiento) ? (DateTime?)null : lector.GetDateTime(posFechaNacimiento),
                        Genero = lector.IsDBNull(posGenero) ? null : lector.GetString(posGenero),
                        Ubicacion = lector.IsDBNull(posUbicacion) ? null : lector.GetString(posUbicacion),
                        Ocupacion = lector.IsDBNull(posOcupacion) ? null : lector.GetString(posOcupacion),
                        Direccion = lector.IsDBNull(posDireccion) ? null : lector.GetString(posDireccion)
                    };
                }
            }
            return usuario;
        }

        public async Task<IPerfilUsuarioResponse> BuscarPerfilUsuarioXNickname(string nickname)
        {

            IPerfilUsuarioResponse usuario = null;
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
                    var posEmail = lector.GetOrdinal("email");
                    var posTelefono = lector.GetOrdinal("telefono");
                    var posFechaNacimiento = lector.GetOrdinal("fecha_nacimiento");
                    var posGenero = lector.GetOrdinal("genero");
                    var posUbicacion = lector.GetOrdinal("ubicacion");
                    var posOcupacion = lector.GetOrdinal("ocupacion");
                    var posDireccion = lector.GetOrdinal("direccion");
                   



                    usuario = new PerfilUsuarioResponse
                    {
                        Id = lector.GetGuidString(posId),
                        Nombre = lector.GetString(posNombre),
                        Apellido = lector.GetString(posApellido),
                        Email = lector.GetString(posEmail),
                        Telefono = lector.IsDBNull(posTelefono) ? null : lector.GetString(posTelefono),
                        FechaNacimiento = lector.IsDBNull(posFechaNacimiento) ? null : lector.GetDateTime(posFechaNacimiento),
                        Genero = lector.IsDBNull(posGenero) ? null : lector.GetString(posGenero),
                        Ubicacion = lector.IsDBNull(posUbicacion) ? null : lector.GetString(posUbicacion),
                        Ocupacion = lector.IsDBNull(posOcupacion) ? null : lector.GetString(posOcupacion),
                        Direccion = lector.IsDBNull(posDireccion) ? null : lector.GetString(posDireccion)
                    };
                }
            }
            return usuario;
        }




        public async Task<IPerfilUsuarioResponse> BuscarPerfilUsuarioXId(string userId)
        {

            IPerfilUsuarioResponse usuario = null;
            var sql = @$"
                SELECT *
                FROM {TablasMysql.USUARIO}
                WHERE id = UNHEX(REPLACE('{userId}', '-', ''));";

            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                if (await lector.ReadAsync())
                {

                    var posId = lector.GetOrdinal("id");
                    var posNombre = lector.GetOrdinal("nombre");
                    var posApellido = lector.GetOrdinal("apellido");
                    var posTelefono = lector.GetOrdinal("telefono");
                    var posEmail = lector.GetOrdinal("email");
                    var posFechaNacimiento = lector.GetOrdinal("fecha_nacimiento");
                    var posGenero = lector.GetOrdinal("genero");
                    var posUbicacion = lector.GetOrdinal("ubicacion");
                    var posOcupacion = lector.GetOrdinal("ocupacion");
                    var posDireccion = lector.GetOrdinal("direccion");

                    usuario = new PerfilUsuarioResponse
                    {
                        Id = lector.GetGuidString(posId),
                        Nombre = lector.GetString(posNombre),
                        Apellido = lector.GetString(posApellido),
                        Email = lector.GetString(posEmail),
                        Telefono = lector.IsDBNull(posTelefono) ? null : lector.GetString(posTelefono),
                        FechaNacimiento = lector.IsDBNull(posFechaNacimiento) ? null : lector.GetDateTime(posFechaNacimiento),
                        Genero = lector.IsDBNull(posGenero) ? null : lector.GetString(posGenero),
                        Ubicacion = lector.IsDBNull(posUbicacion) ? null : lector.GetString(posUbicacion),
                        Ocupacion = lector.IsDBNull(posOcupacion) ? null : lector.GetString(posOcupacion),
                        Direccion = lector.IsDBNull(posDireccion) ? null : lector.GetString(posDireccion)
                    };
                }
            }
            return usuario;
        }



        public async Task ActualizarPerfilUsuario(IPerfilUsuarioRequest request)
        {
            var sql = @$"
    UPDATE {TablasMysql.USUARIO}
    SET 
        nombre = '{request.Nombre}',
        apellido = '{request.Apellido}',
        email = '{request.Email}',
        telefono = '{request.Telefono}',
        fecha_nacimiento = '{request.FechaNacimiento:yyyy-MM-dd}',
        genero = '{request.Genero}',
        ubicacion = '{request.Ubicacion}',
        ocupacion = '{request.Ocupacion}',
        direccion = '{request.Direccion}'
    WHERE id = UNHEX(REPLACE('{request.Id}', '-', ''));
";

            Conexion.IniciarConsulta(sql);
            await Conexion.EjecutarAsync();
        }


        public async Task ActualizarContrasena(string userId, string nuevaContrasena)
        {
            var sql = @$"
        UPDATE {TablasMysql.USUARIO}
        SET contrasenia = '{nuevaContrasena}'
        WHERE id = UNHEX(REPLACE('{userId}', '-', ''));";

            Conexion.IniciarConsulta(sql);
            await Conexion.EjecutarAsync();
        }



    }
}
