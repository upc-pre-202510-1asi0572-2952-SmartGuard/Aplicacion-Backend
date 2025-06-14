using UPC.SmartLock.BE.Hogar.Dto;
using UPC.SmartLock.BE.Hogar.Request;
using UPC.SmartLock.BE.Hogar.Response;

namespace UPC.SmartLock.BL.Homes
{
    public interface IHomeRepositorio
    {
        #region Metodos Sql
        Task InsertarHogar(IHogar value);
        Task EliminarHogarPorId(String hogarId);
        Task<List<IHogarResponse>> GetHogaresPorPropietarioId(String propietarioId);
        Task<IHogarResponse> GetHogarPorId(String propietarioId);
        Task ActualizarHogar(IHogar value);
        //Task<List<IHogarMiembrosResponse>> GetMiembrosAdmitidos(String hogarId);
        //Task InsertarMiembroHogar(IAsociarMiembroRequest value);
        //Task<bool> ExisteMiembroEnHogar(IAsociarMiembroRequest value);
        //Task EliminarMiembroHogar(IAsociarMiembroRequest value);
        #endregion

        #region Metodos TableStorage
        Task InsertarHogarTs(IHogar value);

        #endregion

    }
}
