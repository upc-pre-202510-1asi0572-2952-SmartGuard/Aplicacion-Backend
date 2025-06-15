using UPC.SmartLock.BE.Dispositivos.Dto;
using UPC.SmartLock.BE.Dispositivos.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.DA.Dispositivos
{
    public class DispositivosDa : BaseDA
    {
        #region Constructor
        public DispositivosDa(ConexionMysql conexion) : base(conexion)
        {

        }
        #endregion

        public async Task AgregarNuevoDispositivo(IDispositivo request)
        {
            string query = $@"
                            INSERT INTO {TablasMysql.DISPOSITIVOS} 
                            (id, hogar_id, serie, modelo, porcentaje_bateria, puerta, firmware) 
                            VALUES (
                                UNHEX(REPLACE('{request.Id}', '-', '')),
                                UNHEX(REPLACE('{request.HogarId}', '-', '')),
                                '{request.Serie}',
                                '{request.Modelo}',
                                '{request.Porcentaje}',
                                '{(request.Puerta ? 1 : 0)}',
                                '{request.Firmware}'
                            );
                        ";

            Conexion.IniciarConsulta(query);
            await Conexion.EjecutarAsync();
        }

        public async Task<List<DispositivoResponse>> ObtenerDispositivosXUsuario(string nickname)
        {
            var lista = new List<DispositivoResponse>();
            var sql = @$"
            Select D.id,D.modelo, D.porcentaje_bateria,
                   D.puerta, D.firmware, H.nombre,D.hogar_id 
            from {TablasMysql.DISPOSITIVOS} AS D
            inner join {TablasMysql.HOGAR} as H ON H.id = D.hogar_id
            inner join {TablasMysql.USUARIO} as U on U.id = H.propietario_id
            where U.nickname = '{nickname}';";
            Conexion.IniciarConsulta(sql);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                var postId = lector.GetOrdinal("id");
                var posModelo = lector.GetOrdinal("modelo");
                var posPorcentajeBateria = lector.GetOrdinal("porcentaje_bateria");
                var posPuerta = lector.GetOrdinal("puerta");
                var posFirmware = lector.GetOrdinal("firmware");
                var posHogarNombre = lector.GetOrdinal("nombre");
                var posHogarId = lector.GetOrdinal("hogar_id");

                while (lector.Read())
                {
                    var usuario = new DispositivoResponse();
                    usuario.Id = lector.GetGuidString(postId);
                    usuario.Modelo = lector.GetString(posModelo);
                    usuario.PorcentajeBateria = lector.GetInt32(posPorcentajeBateria);
                    usuario.Puerta = lector.GetBoolean(posPuerta);
                    usuario.Firmware = lector.GetString(posFirmware);
                    usuario.HogarNombre = lector.GetString(posHogarNombre);
                    usuario.HogarId = lector.GetGuidString(posHogarId);
                    lista.Add(usuario);
                }
            }
            return lista;
        }


        //public async Task<IUsuarioResponse> GetUsuarioPorId(int usuarioId)
        //{

        //    IUsuarioResponse usuario = null;
        //    var sql = @$"
        //        SELECT id, nombre, email
        //        FROM {TablasMysql.USUARIO}
        //        WHERE id = {usuarioId};";

        //    Conexion.IniciarConsulta(sql);

        //    using (var lector = await Conexion.EjecutarLectorAsync())
        //    {
        //        if (await lector.ReadAsync())
        //        {

        //            var posId = lector.GetOrdinal("id");
        //            var posNombre = lector.GetOrdinal("nombre");
        //            var posCorreo = lector.GetOrdinal("email");

        //            usuario = new UsuarioResponse
        //            {
        //                Nombre = lector.GetString(posNombre),
        //                Correo = lector.GetString(posCorreo),
        //            };
        //        }
        //    }
        //    return usuario;
        //}
    }
}
