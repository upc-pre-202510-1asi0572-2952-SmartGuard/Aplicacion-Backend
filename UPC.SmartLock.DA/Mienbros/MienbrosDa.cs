using System.Data;
using UPC.SmartLock.BE.Mienbros.Dto;
using UPC.SmartLock.BE.Mienbros.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.DA.Mienbros
{
    public class MienbrosDa : BaseDA
    {
        #region Constructor
        public MienbrosDa(ConexionMysql conexion) : base(conexion)
        {

        }
        #endregion

        #region Metodos

        public async Task AsociarMienbroAHogar(IMienbroHogarAsociado request)
        {
            string query = @$" INSERT INTO {TablasMysql.MIEMBROS_HABILITADO_HOGAR} (id,hogar_id,mienbro_id,estatus) VALUES 
                               ( UNHEX(REPLACE('{request.Id}', '-', '')),
                                 UNHEX(REPLACE('{request.HogarId}', '-', '')),
                                 UNHEX(REPLACE('{request.MienbroId}', '-', '')),
                                 '{(request.Estatus ? 1 : 0)}');";
            Conexion.IniciarConsulta(query);
            await Conexion.EjecutarAsync();
        }

        public async Task AgregarNuevoMienbro(IMienbro request)
        {
            string query = @$"
                            INSERT INTO {TablasMysql.MIEMBROS}
                            (id, nombre, edad, parentesco, descripcion, foto_perfil, user_id)
                            VALUES (
                                UNHEX(REPLACE('{request.Id}', '-', '')),
                                '{request.Nombre}', {request.Edad}, '{request.Parentesco}',
                                {(request.Descripcion != null ? $"'{request.Descripcion}'" : "NULL")},
                                {(request.FotoPerfil != null ? $"'{request.FotoPerfil}'" : "NULL")},
                                UNHEX(REPLACE('{request.UserId}', '-', '')));";

            Conexion.IniciarConsulta(query);
            await Conexion.EjecutarAsync();
        }


        public async Task<List<IMienbroResponse>> ObtenerMiembrosPorPropietarioId(String propietarioId)
        {
            var lista = new List<IMienbroResponse>();
            var sql = @$"
        SELECT         
        HEX(id) as id,
        nombre,
        edad,
        parentesco,
        descripcion,
        foto_perfil,
        HEX(user_id) as user_id
        FROM {TablasMysql.MIEMBROS}
        WHERE user_id = UNHEX(REPLACE('{propietarioId}', '-', ''));";

            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                while (lector.Read())
                {
                    var miembro = new MienbroResponse
                    {
                        Id = lector.GetString("id"),
                        Nombre = lector.GetString("nombre"),
                        Edad = lector.GetInt32("edad"),
                        Parentesco = lector.GetString("parentesco"),
                        Descripcion = lector.GetString("descripcion"),
                        FotoPerfil = lector.GetString("foto_perfil"),
                        UserId = lector.GetString("user_id"),
                    };

                    lista.Add(miembro);
                }
            }

            return lista;
        }


        public async Task<List<IMienbroResponse>> ObtenerMiembrosHabilitadosPorHogarId(string hogarId)
        {
            var lista = new List<IMienbroResponse>();
            var sql = @$"
        SELECT 
            HEX(MU.id) as id,
            MU.nombre,
            MU.edad,
            MU.parentesco,
            MU.descripcion,
            MU.foto_perfil,
            HEX(MU.user_id) as user_id
        FROM {TablasMysql.MIEMBROS} MU
        INNER JOIN {TablasMysql.MIEMBROS_HABILITADO_HOGAR} MHH 
            ON MU.id = MHH.mienbro_id
        WHERE MHH.hogar_id = UNHEX(REPLACE('{hogarId}', '-', ''))
          AND MHH.estatus = 1;";

            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                while (lector.Read())
                {
                    var miembro = new MienbroResponse
                    {
                        Id = lector.GetString("id"),
                        Nombre = lector.GetString("nombre"),
                        Edad = lector.GetInt32("edad"),
                        Parentesco = lector.GetString("parentesco"),
                        Descripcion = lector.GetString("descripcion"),
                        FotoPerfil = lector.GetString("foto_perfil"),
                        UserId = lector.GetString("user_id")
                    };

                    lista.Add(miembro);
                }
            }

            return lista;
        }



        public async Task<IMienbroResponse> BuscarMiembroPorId(string miembroId)
        {
            IMienbroResponse miembro = null;
            var sql = @$"
        SELECT 
            HEX(id) as id,
            nombre,
            edad,
            parentesco,
            descripcion,
            foto_perfil,
            HEX(user_id) as user_id
        FROM {TablasMysql.MIEMBROS}
        WHERE id = UNHEX(REPLACE('{miembroId}', '-', ''));";

            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                if (lector.Read())
                {
                    miembro = new MienbroResponse
                    {
                        Id = lector.GetString("id"),
                        Nombre = lector.GetString("nombre"),
                        Edad = lector.GetInt32("edad"),
                        Parentesco = lector.GetString("parentesco"),
                        Descripcion = lector.GetString("descripcion"),
                        FotoPerfil = lector.GetString("foto_perfil"),
                        UserId = lector.GetString("user_id")
                    };
                }
            }

            return miembro;
        }

        public async Task<MienbroInfoTemporalResponse> ObtenerEstadoInformacionMienbro(string idMienbro)
        {
            var info = new MienbroInfoTemporalResponse();
            var sql = @$"
                    select 
                            MU.nombre as NombreMienbro, MU.parentesco, MU.edad,MU.foto_perfil, 
                            MHH.estatus, H.nombre as NombreHogar,H.tipo_propiedad
                    from {TablasMysql.MIEMBROS} AS MU
                    inner join {TablasMysql.MIEMBROS_HABILITADO_HOGAR} as MHH  on MU.id = MHH.mienbro_id
                    inner join {TablasMysql.HOGAR} as H on H.id = MHH.hogar_id 
                    where MU.id = UNHEX('{idMienbro}');";

            Conexion.IniciarConsulta(sql);
            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                var posNombreMienbro = lector.GetOrdinal("NombreMienbro");
                var posParentesco = lector.GetOrdinal("parentesco");
                var posEdad = lector.GetOrdinal("edad");
                var posFotoPerfil = lector.GetOrdinal("foto_perfil");
                var posEstatus = lector.GetOrdinal("estatus");
                var posNombreHogar = lector.GetOrdinal("NombreHogar");
                var posTipoPropiedad = lector.GetOrdinal("tipo_propiedad");

                while (lector.Read())
                {
                    info.MienbroNombre = lector.GetString(posNombreHogar);
                    info.Parentesco = lector.GetString(posNombreHogar);
                    info.Edad = lector.GetInt32(posEdad);
                    info.FotoPerfil = lector.GetString(posFotoPerfil);
                    info.Estatus = lector.GetInt32(posEstatus);
                    info.HogarNombre = lector.GetString(posNombreHogar);
                    info.TipoHogar = lector.GetString(posTipoPropiedad);
                }
            }
            return info;
        }


        public async Task ActualizarMiembro(IMienbro request)
        {
            var sql = @$"
        UPDATE {TablasMysql.MIEMBROS}
        SET 
            nombre = '{request.Nombre}',
            edad = {request.Edad},
            parentesco = '{request.Parentesco}',
            descripcion = '{request.Descripcion}',
            foto_perfil = '{request.FotoPerfil}'
        WHERE id = UNHEX(REPLACE('{request.Id}', '-', ''));";

            Conexion.IniciarConsulta(sql);
            await Conexion.EjecutarAsync();
        }

        public async Task EliminarMiembroPorId(string miembroId)
        {
            var sql = @$"
        DELETE FROM {TablasMysql.MIEMBROS}
        WHERE id = UNHEX(REPLACE('{miembroId}', '-', ''));";

            Conexion.IniciarConsulta(sql);
            await Conexion.EjecutarAsync();
        }
        #endregion
    }
}
