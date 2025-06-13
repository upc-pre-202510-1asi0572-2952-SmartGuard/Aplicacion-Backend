using UPC.SmartLock.BE.Dispositivos.Dto;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.DA.Dispositivos;

namespace UPC.SmartLock.BL.Dipositivos
{
    public class DipositivoRepositorio : IDipositivoRepositorio
    {
        private Repositorio _repositorio;

        public DipositivoRepositorio(Repositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task InsertarDispositivo(Dispositivo value)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                Conexion.IniciarTransaccion();
                try
                {
                    var data = new DispositivosDa(Conexion);
                    await data.AgregarNuevoDispositivo(value);
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
