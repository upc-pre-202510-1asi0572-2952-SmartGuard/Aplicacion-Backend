using UPC.SmartLock.BE.Dispositivos.Dto;
using UPC.SmartLock.BE.Dispositivos.Response;

namespace UPC.SmartLock.BL.Dipositivos
{
    public interface IDipositivoRepositorio
    {
        Task InsertarDispositivo(Dispositivo value);
        Task<List<DispositivoResponse>> ObtenerDispositivosXUsuario(string nickname);
    }
}
