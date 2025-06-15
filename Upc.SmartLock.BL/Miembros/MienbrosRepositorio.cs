using UPC.SmartLock.BE.Mienbros.Dto;
using UPC.SmartLock.BE.Mienbros.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.DA.Mienbros;

namespace UPC.SmartLock.BL.Miembros
{
    public class MienbrosRepositorio : IMienbrosRepositorio
    {
        private Repositorio _repositorio;
        public MienbrosRepositorio(Repositorio Repositorio)
        {
            _repositorio = Repositorio;
        }

        public async Task AsociarMienbroAHogar(IMienbroHogarAsociado request)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                Conexion.IniciarTransaccion();
                try
                {
                    var data = new MienbrosDa(Conexion);
                    await data.AsociarMienbroAHogar(request);
                    Conexion.EjecutarTransaccion();
                }
                catch (Exception ex)
                {
                    Conexion.CancelarTransaccion();
                    throw new BE.Util.MensajeExceptionExtendido(ex.Message);
                }
            }
        }

        public async Task InsertarMienbroxUsuario(IMienbro value)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                Conexion.IniciarTransaccion();
                try
                {
                    var data = new MienbrosDa(Conexion);
                    await data.AgregarNuevoMienbro(value);
                    Conexion.EjecutarTransaccion();
                }
                catch (Exception ex)
                {
                    Conexion.CancelarTransaccion();
                    throw new BE.Util.MensajeExceptionExtendido(ex.Message);
                }
            }
        }


        public async Task<List<IMienbroResponse>> GetMiembrosPorPropietarioId(String propietarioId)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                var data = new MienbrosDa(Conexion);
                return await data.ObtenerMiembrosPorPropietarioId(propietarioId);
            }

        }

        public async Task<List<IMienbroResponse>> GetMiembrosHabilitadosPorHogarId(String hogarId)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                var data = new MienbrosDa(Conexion);
                return await data.ObtenerMiembrosHabilitadosPorHogarId(hogarId);
            }

        }


        public async Task<IMienbroResponse> GetMiembroPorId(String miembroId)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                var data = new MienbrosDa(Conexion);
                return await data.BuscarMiembroPorId(miembroId);
            }
        }

        public async Task ActualizarMiembro(IMienbro value)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {

                Conexion.IniciarTransaccion();
                try
                {
                    var data = new MienbrosDa(Conexion);
                    await data.ActualizarMiembro(value);
                    Conexion.EjecutarTransaccion();
                }
                catch (Exception ex)
                {
                    Conexion.CancelarTransaccion();
                    throw new BE.Util.MensajeExceptionExtendido(ex.Message);
                }

            }
        }


        public async Task EliminarMiembroPorId(String miembroId)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                Conexion.IniciarTransaccion();
                try
                {
                    var data = new MienbrosDa(Conexion);
                    await data.EliminarMiembroPorId(miembroId);
                    Conexion.EjecutarTransaccion();
                }
                catch (Exception ex)
                {
                    Conexion.CancelarTransaccion();
                    throw new BE.Util.MensajeExceptionExtendido(ex.Message);
                }
            }
        }


        public async Task<MienbroInfoTemporalResponse> ObtenerInfoMienbroTemporal(string idMienbro)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                var data = new MienbrosDa(Conexion);
                return await data.ObtenerEstadoInformacionMienbro(idMienbro);
            }
        }
    }
}
