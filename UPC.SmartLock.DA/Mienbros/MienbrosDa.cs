using UPC.SmartLock.BE.Dispositivos.Request;
using UPC.SmartLock.BE.Mienbros.Request;
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

        public async Task AgregarNuevoDispositivo(IMienbroRequest request)
        {
            string query = @$"
                            INSERT INTO {TablasMysql.MIEMBROS}
                            (id, nombre, edad, parentesco, descripcion, foto_perfil, hogar_id, user_id)
                            VALUES (
                                UNHEX(REPLACE('{request.Id}', '-', '')),
                                '{request.Nombre}',
                                {request.Edad},
                                '{request.Parentesco}',
                                {(request.Descripcion != null ? $"'{request.Descripcion}'" : "NULL")},
                                {(request.FotoPerfil != null ? $"'{request.FotoPerfil}'" : "NULL")},
                                {(request.HogarId != null ? $"UNHEX(REPLACE('{request.HogarId}', '-', ''))" : "NULL")},
                                UNHEX(REPLACE('{request.UserId}', '-', ''))
                            );
                        ";

            Conexion.IniciarConsulta(query);
            await Conexion.EjecutarAsync();
        }
    }
}
