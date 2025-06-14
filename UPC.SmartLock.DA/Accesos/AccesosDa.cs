using System.Data;
using UPC.SmartLock.BE.Accesos.Request;
using UPC.SmartLock.BE.Hogar.Response;
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

        public async Task ObtenerAccesosXPorUsuario(string userId)
        {
            string query = @$"
            SELECT * 
            FROM {TablasMysql.ACCESOS_HISTORICOS} AS AH
            inner join hogares AS H on H.id = AH.hogar_id	
            inner join usuarios as U on U.id = H.propietario_id;
            ";
            Conexion.IniciarConsulta(query);

            using (var lector = await Conexion.EjecutarLectorAsync())
            {
                while (lector.Read())
                {
                    //var hogar = new HogarResponse
                    //{
                    //    Id = lector.GetString("id"),
                    //    Nombre = lector.GetString("nombre"),
                    //    Direccion = lector.GetString("direccion"),
                    //    ImgUrl = lector.GetString("url_img"),
                    //    TipoPropiedad = lector.GetString("tipo_propiedad"),
                    //    Habitaciones = lector.GetInt32("habitaciones"),
                    //    Baños = lector.GetInt32("baños"),
                    //    Calefaccion = lector.GetBoolean("calefaccion"),
                    //    AbastecimientoAgua = lector.GetString("abastecimiento_agua"),
                    //    ProveedorInternet = lector.GetString("proveedor_internet"),
                    //    SistemaSeguridad = lector.GetString("sistema_seguridad"),
                    //    FuncionesInteligentes = lector.GetInt32("funciones_inteligentes"),
                    //    PropietarioId = lector.GetString("propietario_id")
                    //};

                    //lista.Add(hogar);
                }
            }


        }

    }
}
