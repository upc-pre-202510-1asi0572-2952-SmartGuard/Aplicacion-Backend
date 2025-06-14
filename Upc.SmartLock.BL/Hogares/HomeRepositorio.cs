using UPC.SmartLock.BE.Hogar.Dto;
using UPC.SmartLock.BE.Hogar.Request;
using UPC.SmartLock.BE.Hogar.Response;
using UPC.SmartLock.BE.Usuario.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.DA.Homes;

namespace UPC.SmartLock.BL.Homes
{
    public class HomeRepositorio : IHomeRepositorio
    {
        private Repositorio _repositorio;
        public HomeRepositorio(Repositorio Repositorio)
        {
            _repositorio = Repositorio;
        }


        #region Metodos Mysql
        public async Task<List<IHogarResponse>> GetHogaresPorPropietarioId(String propietarioId)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                var data = new HogarDa(Conexion);
                return await data.ObtenerHogaresPorPropietarioId(propietarioId);
            }
        }


        public async Task InsertarHogar(IHogar value)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                Conexion.IniciarTransaccion();
                try
                {
                    var data = new HogarDa(Conexion);
                    await data.AgregarNuevoHogar(value);
                    Conexion.EjecutarTransaccion();
                }
                catch (Exception ex)
                {
                    Conexion.CancelarTransaccion();
                    throw new BE.Util.MensajeExceptionExtendido(ex.Message);
                }
            }

        }

        public async Task EliminarHogarPorId(String hogarId)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                Conexion.IniciarTransaccion();
                try
                {
                    var data = new HogarDa(Conexion);
                    await data.EliminarHogarPorId(hogarId);
                    Conexion.EjecutarTransaccion();
                }
                catch (Exception ex)
                {
                    Conexion.CancelarTransaccion();
                    throw new BE.Util.MensajeExceptionExtendido(ex.Message);
                }
            }

        }

        public async Task<IHogarResponse> GetHogarPorId(String hogarId)
        {

            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {
                var data = new HogarDa(Conexion);
                return await data.BuscarHogarPorId(hogarId);
            }
        }

        public async Task ActualizarHogar(IHogar value)
        {
            using (var Conexion = new ConexionMysql(_repositorio.CadenaConexion))
            {

                Conexion.IniciarTransaccion();
                try
                {
                    var data = new HogarDa(Conexion);
                    await data.ActualizarHogar(value);
                    Conexion.EjecutarTransaccion();
                }
                catch (Exception ex)
                {
                    Conexion.CancelarTransaccion();
                    throw new BE.Util.MensajeExceptionExtendido(ex.Message);
                }

            }
        }

        //public async Task<List<IHogarMiembrosResponse>> GetMiembrosAdmitidos(int hogarId)
        //{
        //    using (var conexion = new ConexionMysql(_repositorio.CadenaConexion))
        //    {
        //        var data = new HogarDa(conexion);
        //        return await data.ObtenerMiembrosAdmitidos(hogarId);
        //    }
        //}

        //public async Task<bool> ExisteMiembroEnHogar(IAsociarMiembroRequest value)
        //{
        //    using (var conexion = new ConexionMysql(_repositorio.CadenaConexion))
        //    {
        //        var data = new HogarDa(conexion);
        //        return await data.ExisteMiembroEnHogar(value);
        //    }
        //}

        //public async Task InsertarMiembroHogar(IAsociarMiembroRequest value)
        //{
        //    using (var conexion = new ConexionMysql(_repositorio.CadenaConexion))
        //    {
        //        var data = new HogarDa(conexion);
        //        await data.InsertarMiembroHogar(value);
        //    }
        //}

        //public async Task EliminarMiembroHogar(IAsociarMiembroRequest value)
        //{
        //    using var conexion = new ConexionMysql(_repositorio.CadenaConexion);
        //    conexion.IniciarTransaccion();
        //    try
        //    {
        //        var data = new HogarDa(conexion);
        //        await data.EliminarMiembroHogar(value);
        //        conexion.EjecutarTransaccion();
        //    }
        //    catch (Exception)
        //    {
        //        conexion.CancelarTransaccion();
        //        throw;
        //    }
        //}
        #endregion


        #region Metodos Table Storage


        public async Task InsertarHogarTs(IHogar value)
        {
            var data = new HogarTs(_repositorio.Almacenamiento);
            await data.InsertarAsync(value);
        }

        #endregion

    }
}
