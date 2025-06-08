using UPC.SmartLock.BE.Hogar.Response;
using UPC.SmartLock.BE.Hogar.Dto;
using UPC.SmartLock.BE.Hogar.Request;
using UPC.SmartLock.BE.Usuario.Response;

namespace UPC.SmartLock.BL.Homes
{
    public interface IHomeRepositorio
    {
        #region Metodos Sql
        Task InsertarHogar(IHogarRequest value);
        Task<List<IHogarResponse>> GetHogaresPorPropietarioId(int propietarioId);
        Task<IHogarResponse> GetHogarPorId(int propietarioId);
        Task ActualizarHogar(IHogarRequest value);
        Task<List<IHogarMiembrosResponse>> GetMiembrosAdmitidos(int hogarId);
        Task InsertarMiembroHogar(IAsociarMiembroRequest value);
        Task<bool> ExisteMiembroEnHogar(IAsociarMiembroRequest value);
        Task EliminarMiembroHogar(IAsociarMiembroRequest value);
        #endregion

        #region Metodos TableStorage
        Task InsertarHogarTs(IHogar value);
   
        #endregion

    }
}
