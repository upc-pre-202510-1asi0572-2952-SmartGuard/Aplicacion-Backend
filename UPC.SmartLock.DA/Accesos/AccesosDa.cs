using UPC.SmartLock.BE.Accesos.Request;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.DA.Accesos
{
    public class AccesosDa : BaseDA
    {
        #region Constructor
        public AccesosDa(ConexionMysql conexion) : base(conexion)
        {

        }
        #endregion

        public async Task AgregarRegistroAcceso(IAccesosRequest request)
        {
            string query = @$"
                    INSERT INTO {TablasMysql.ACCESOS_REGISTRADOS}
                    (id, hogar_id, fecha_hora, resultado)
                    VALUES (
                        UNHEX(REPLACE('{request.Id}', '-', '')),
                        UNHEX(REPLACE('{request.HogarId}', '-', '')),
                        '{request.FechaHora}',
                        '{request.Resultado}');";

            Conexion.IniciarConsulta(query);
            await Conexion.EjecutarAsync();
        }

    }
}
