using UPC.SmartLock.BE.Hogar.Dto;
using UPC.SmartLock.BE.Hogar.Response;
using UPC.SmartLock.BE.Mienbros.Dto;
using UPC.SmartLock.BE.Mienbros.Response;

namespace UPC.SmartLock.BL.Miembros
{
    public interface IMienbrosRepositorio
    {
        Task InsertarMienbroxUsuario(IMienbro value);

        Task AsociarMienbroAHogar(IMienbroHogarAsociado request);
        Task<List<IMienbroResponse>> GetMiembrosPorPropietarioId(String propietarioId);

        Task EliminarMiembroPorId(String miembroId);

        Task<IMienbroResponse> GetMiembroPorId(String miembroId);
        Task ActualizarMiembro(IMienbro value);

        Task<MienbroInfoTemporalResponse> ObtenerInfoMienbroTemporal(string idMienbro);

    }
}
