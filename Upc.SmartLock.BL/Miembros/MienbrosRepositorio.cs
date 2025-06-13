using Google.Protobuf.WellKnownTypes;
using UPC.SmartLock.BE.Mienbros.Dto;
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
    }
}
