using System.Data;
using UPC.SmartLock.BE.Hogar.Dto;
using UPC.SmartLock.BE.Hogar.Request;
using UPC.SmartLock.BE.Hogar.Response;
using UPC.SmartLock.BE.Usuario.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.DA.Homes
{
    public class HogarDa : BaseDA
    {
        #region Constructor
        public HogarDa(ConexionMysql conexion) : base(conexion)
        {

        }
        #endregion

        public async Task AgregarNuevoHogar(IHogar request)
        {
            string query = @$"
        INSERT INTO {TablasMysql.HOGAR} (
            id, nombre, direccion, url_img, tipo_propiedad, habitaciones, baños,
            calefaccion, abastecimiento_agua, proveedor_internet, sistema_seguridad,
            funciones_inteligentes, propietario_id
        )
        VALUES (
            UNHEX(REPLACE('{request.Id}', '-', '')),
            '{request.Nombre}',
            '{request.Direccion}',
            '{request.ImgUrl}',
            '{request.TipoPropiedad}',
            {request.Habitaciones},
            {request.Baños},
            {(request.Calefaccion ? 1 : 0)},
            '{request.AbastecimientoAgua}',
            '{request.ProveedorInternet}',
            '{request.SistemaSeguridad}',
            {request.FuncionesInteligentes},
            UNHEX(REPLACE('{request.PropietarioId}', '-', ''))
        );";

            Conexion.IniciarConsulta(query);
            await Conexion.EjecutarAsync();
        }


        public async Task EliminarHogarPorId(string hogarId)
        {
            var sql = @$"
        DELETE FROM {TablasMysql.HOGAR}
        WHERE id = UNHEX(REPLACE('{hogarId}', '-', ''));";

            Conexion.IniciarConsulta(sql);
            await Conexion.EjecutarAsync();
        }

        public async Task<List<IHogarResponse>> ObtenerHogaresPorPropietarioId(String propietarioId)
        {
            var lista = new List<IHogarResponse>();
            var sql = @$"
        SELECT 
            HEX(id) as id,
            nombre,
            direccion,
            url_img,
            tipo_propiedad,
            habitaciones,
            baños,
            calefaccion,
            abastecimiento_agua,
            proveedor_internet,
            sistema_seguridad,
            funciones_inteligentes,
            HEX(propietario_id) as propietario_id
        FROM {TablasMysql.HOGAR}
        WHERE propietario_id = UNHEX(REPLACE('{propietarioId}', '-', ''));";

            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                while (lector.Read())
                {
                    var hogar = new HogarResponse
                    {
                        Id = lector.GetString("id"),
                        Nombre = lector.GetString("nombre"),
                        Direccion = lector.GetString("direccion"),
                        ImgUrl = lector.GetString("url_img"),
                        TipoPropiedad = lector.GetString("tipo_propiedad"),
                        Habitaciones = lector.GetInt32("habitaciones"),
                        Baños = lector.GetInt32("baños"),
                        Calefaccion = lector.GetBoolean("calefaccion"),
                        AbastecimientoAgua = lector.GetString("abastecimiento_agua"),
                        ProveedorInternet = lector.GetString("proveedor_internet"),
                        SistemaSeguridad = lector.GetString("sistema_seguridad"),
                        FuncionesInteligentes = lector.GetInt32("funciones_inteligentes"),
                        PropietarioId = lector.GetString("propietario_id")
                    };

                    lista.Add(hogar);
                }
            }

            return lista;
        }

        public async Task<IHogarResponse> BuscarHogarPorId(string hogarId)
        {
            IHogarResponse hogar = null;
            var sql = @$"
        SELECT 
            HEX(id) as id,
            nombre,
            direccion,
            url_img,
            tipo_propiedad,
            habitaciones,
            baños,
            calefaccion,
            abastecimiento_agua,
            proveedor_internet,
            sistema_seguridad,
            funciones_inteligentes,
            HEX(propietario_id) as propietario_id
        FROM {TablasMysql.HOGAR}
        WHERE id = UNHEX(REPLACE('{hogarId}', '-', ''));";

            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                if (lector.Read())
                {
                    hogar= new HogarResponse
                    {
                        Id = lector.GetString("id"),
                        Nombre = lector.GetString("nombre"),
                        Direccion = lector.GetString("direccion"),
                        ImgUrl = lector.GetString("url_img"),
                        TipoPropiedad = lector.GetString("tipo_propiedad"),
                        Habitaciones = lector.GetInt32("habitaciones"),
                        Baños = lector.GetInt32("baños"),
                        Calefaccion = lector.GetBoolean("calefaccion"),
                        AbastecimientoAgua = lector.GetString("abastecimiento_agua"),
                        ProveedorInternet = lector.GetString("proveedor_internet"),
                        SistemaSeguridad = lector.GetString("sistema_seguridad"),
                        FuncionesInteligentes = lector.GetInt32("funciones_inteligentes"),
                        PropietarioId = lector.GetString("propietario_id")
                    };
                }
            }

            return hogar;
        }




        public async Task ActualizarHogar(IHogar request)
        {
            var sql = @$"
        UPDATE {TablasMysql.HOGAR}
        SET 
            nombre = '{request.Nombre}',
            direccion = '{request.Direccion}',
            url_img = '{request.ImgUrl}',
            tipo_propiedad = '{request.TipoPropiedad}',
            habitaciones = {request.Habitaciones},
            baños = {request.Baños},
            calefaccion = {(request.Calefaccion ? 1 : 0)},
            abastecimiento_agua = '{request.AbastecimientoAgua}',
            proveedor_internet = '{request.ProveedorInternet}',
            sistema_seguridad = '{request.SistemaSeguridad}',
            funciones_inteligentes = {request.FuncionesInteligentes}
        WHERE id = UNHEX(REPLACE('{request.Id}', '-', ''));";
            Conexion.IniciarConsulta(sql);
            await Conexion.EjecutarAsync();
        }

        //public async Task<List<IHogarMiembrosResponse>> ObtenerMiembrosAdmitidos(int hogarId)
        //{
        //    var lista = new List<IHogarMiembrosResponse>();
        //    var sql = @$"
        //SELECT u.id, u.nombre, u.apellido, u.email, u.mote
        //FROM {TablasMysql.MIEMBROS} h
        //INNER JOIN {TablasMysql.USUARIO} u ON h.user_id = u.id
        //WHERE h.hogar_id = {hogarId};";

        //    Conexion.IniciarConsulta(sql);

        //    using (var lector = await Conexion.EjecutarLectorAsync())
        //    {
        //        var posId = lector.GetOrdinal("id");
        //        var posNombre = lector.GetOrdinal("nombre");
        //        var posApellido = lector.GetOrdinal("apellido");
        //        var posEmail = lector.GetOrdinal("email");
        //        var posMote = lector.GetOrdinal("mote");

        //        while (lector.Read())
        //        {
        //            var hogarMiembros = new HogarMiembrosResponse();
        //            hogarMiembros.Id = lector.GetInt32(posId);
        //            hogarMiembros.Nombre = lector.GetString(posNombre);
        //            hogarMiembros.Apellido = lector.GetString(posApellido);
        //            hogarMiembros.Email = lector.GetString(posEmail);
        //            hogarMiembros.Mote = lector.GetString(posMote);
        //            lista.Add(hogarMiembros);

        //        }

        //    }

        //    return lista;
        //}

        //public async Task InsertarMiembroHogar(IAsociarMiembroRequest request)
        //{
        //    var sql = @$"
        //INSERT INTO {TablasMysql.MIEMBROS} (hogar_id, user_id)
        //VALUES ({request.HogarId}, {request.UserId});";

        //    Conexion.IniciarConsulta(sql);
        //    await Conexion.EjecutarAsync();
        //}

        //public async Task<bool> ExisteMiembroEnHogar(IAsociarMiembroRequest request)
        //{
        //    var sql = @$"
        //SELECT COUNT(*) 
        //FROM {TablasMysql.MIEMBROS} 
        //WHERE hogar_id = {request.HogarId} AND user_id = {request.UserId};";

        //    Conexion.IniciarConsulta(sql);
        //    var resultado = await Conexion.EjecutarValorAsync();

        //    return Convert.ToInt32(resultado) > 0;
        //}
        //public async Task EliminarMiembroHogar(IAsociarMiembroRequest request)
        //{
        //    string sql = @$"
        //    DELETE FROM {TablasMysql.MIEMBROS} 
        //    WHERE hogar_id = {request.HogarId} AND user_id = {request.UserId};";

        //    Conexion.IniciarConsulta(sql);
        //    await Conexion.EjecutarAsync();
        //}


    }
}
