using UPC.SmartLock.BE.Dispositivos.Dto;

namespace UPC.SmartLock.BL.Dipositivos
{
    public interface IDipositivoRepositorio
    {
        Task InsertarDispositivo(Dispositivo value);
    }
}
