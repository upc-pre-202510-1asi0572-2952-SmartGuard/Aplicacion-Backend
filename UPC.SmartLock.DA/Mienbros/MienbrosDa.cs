using System.Data;
using UPC.SmartLock.BE.Dispositivos.Request;
using UPC.SmartLock.BE.Hogar.Response;
using UPC.SmartLock.BE.Mienbros.Dto;
using UPC.SmartLock.BE.Mienbros.Request;
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
                            (id, nombre, edad, parentesco, descripcion, foto_perfil, hogar_id, user_id)
                            VALUES (
                                UNHEX(REPLACE('{request.Id}', '-', '')),
                                '{request.Nombre}', {request.Edad}, '{request.Parentesco}',
                                {(request.Descripcion != null ? $"'{request.Descripcion}'" : "NULL")},
                                {(request.FotoPerfil != null ? $"'{request.FotoPerfil}'" : "NULL")},
                                {(request.HogarId != null ? $"UNHEX(REPLACE('{request.HogarId}', '-', ''))" : "NULL")},
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
        HEX(hogar_id) as hogar_id,
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
                        HogarId = lector.IsDBNull(lector.GetOrdinal("hogar_id")) ? null : lector.GetString("hogar_id"),
                        UserId = lector.GetString("user_id"),
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
            HEX(hogar_id) as hogar_id,
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
                        HogarId = lector.IsDBNull("hogar_id") ? null : lector.GetString("hogar_id"),
                        UserId = lector.GetString("user_id")
                    };
                }
            }

            return miembro;
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
