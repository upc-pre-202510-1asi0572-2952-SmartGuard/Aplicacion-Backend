using UPC.SmartLock.BE.Hogar.Dto;
using UPC.SmartLock.BE.Hogar.Request;
using UPC.SmartLock.BE.Hogar.Response;
using UPC.SmartLock.BE.Usuario.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.DA.Homes
{
    public class HomesDa : BaseDA
    {
        #region Constructor
        public HomesDa(ConexionMysql conexion) : base(conexion)
        {

        }
        #endregion

        public async Task AgregarNuevoHogar(IHogar request)
        {

            string query = @$" INSERT INTO {TablasMysql.HOGAR} (id,direccion, nombre, propietario_id)
                              VALUES (UNHEX(REPLACE('{request.Id}', '-', '')),'{request.Direccion}', '{request.Nombre}', UNHEX(REPLACE('{request.PropietarioId}', '-', ''))); ";
            Conexion.IniciarConsulta(query);
            await Conexion.EjecutarAsync();
        }

        public async Task<List<IHogarResponse>> ObtenerHogaresPorPropietarioId(int propietarioId)
        {
            var lista = new List<IHogarResponse>();
            var sql = @$"
                SELECT id, direccion, nombre, propietario_id
                FROM {TablasMysql.HOGAR}
                WHERE propietario_id = {propietarioId};";

            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                var posId = lector.GetOrdinal("id");
                var posNombre = lector.GetOrdinal("nombre");
                var posDireccion = lector.GetOrdinal("direccion");
                var posPropietarioId = lector.GetOrdinal("propietario_id");

                while (lector.Read())
                {
                    var hogar = new HogarResponse();
                    hogar.Id = lector.GetInt32(posId);
                    hogar.Nombre = lector.GetString(posNombre);
                    hogar.Direccion = lector.GetString(posDireccion);
                    hogar.PropietarioId = lector.GetInt32(posPropietarioId);

                    lista.Add(hogar);
                }
            }

            return lista;
        }

        public async Task<IHogarResponse> GetHogarPorId(int hogarId)
        {
    
            IHogarResponse hogar = null;
            var sql = @$"
                SELECT id, direccion, nombre, propietario_id
                FROM {TablasMysql.HOGAR}
                WHERE id = {hogarId};";

            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                if (await lector.ReadAsync()) 
                {

                    var posId = lector.GetOrdinal("id");
                    var posDireccion = lector.GetOrdinal("direccion");
                    var posNombre = lector.GetOrdinal("nombre");
                    var posPropietarioId = lector.GetOrdinal("propietario_id");

                    hogar = new HogarResponse
                    {
                        Id = lector.GetInt32(posId),
                        Direccion = lector.GetString(posDireccion),
                        Nombre = lector.GetString(posNombre),
                        PropietarioId=lector.GetInt32(posPropietarioId)
                    };
                }
            }
            return hogar; 
        }

        //public async Task ActualizarHogar(IHogarRequest request)
        //{
        //    var sql = @$"
        //    UPDATE {TablasMysql.HOGAR}
        //    SET 
        //    direccion = '{request.Direccion}', 
        //    nombre = '{request.Nombre}'
        //    WHERE id = {request.Id};";

        //    Conexion.IniciarConsulta(sql);
        //    await Conexion.EjecutarAsync();
        //}

        public async Task<List<IHogarMiembrosResponse>> ObtenerMiembrosAdmitidos(int hogarId)
        {
            var lista = new List<IHogarMiembrosResponse>();
            var sql = @$"
        SELECT u.id, u.nombre, u.apellido, u.email, u.mote
        FROM {TablasMysql.MIEMBROS} h
        INNER JOIN {TablasMysql.USUARIO} u ON h.user_id = u.id
        WHERE h.hogar_id = {hogarId};";

            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                var posId = lector.GetOrdinal("id");
                var posNombre = lector.GetOrdinal("nombre");
                var posApellido = lector.GetOrdinal("apellido");
                var posEmail = lector.GetOrdinal("email");
                var posMote = lector.GetOrdinal("mote");

                while (lector.Read())
                {
                    var hogarMiembros = new HogarMiembrosResponse();
                    hogarMiembros.Id = lector.GetInt32(posId);
                    hogarMiembros.Nombre = lector.GetString(posNombre);
                    hogarMiembros.Apellido = lector.GetString(posApellido);
                    hogarMiembros.Email = lector.GetString(posEmail);
                    hogarMiembros.Mote = lector.GetString(posMote);
                    lista.Add(hogarMiembros);

                }

            }

            return lista;
        }

        public async Task InsertarMiembroHogar(IAsociarMiembroRequest request)
        {
            var sql = @$"
        INSERT INTO {TablasMysql.MIEMBROS} (hogar_id, user_id)
        VALUES ({request.HogarId}, {request.UserId});";

            Conexion.IniciarConsulta(sql);
            await Conexion.EjecutarAsync();
        }

        public async Task<bool> ExisteMiembroEnHogar(IAsociarMiembroRequest request)
        {
            var sql = @$"
        SELECT COUNT(*) 
        FROM {TablasMysql.MIEMBROS} 
        WHERE hogar_id = {request.HogarId} AND user_id = {request.UserId};";

            Conexion.IniciarConsulta(sql);
            var resultado = await Conexion.EjecutarValorAsync();

            return Convert.ToInt32(resultado) > 0;
        }
        public async Task EliminarMiembroHogar(IAsociarMiembroRequest request)
        {
            string sql = @$"
            DELETE FROM {TablasMysql.MIEMBROS} 
            WHERE hogar_id = {request.HogarId} AND user_id = {request.UserId};";

            Conexion.IniciarConsulta(sql);
            await Conexion.EjecutarAsync();
        }


    }
}
