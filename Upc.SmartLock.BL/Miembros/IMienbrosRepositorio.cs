using UPC.SmartLock.BE.Mienbros.Dto;

namespace UPC.SmartLock.BL.Miembros
{
    public interface IMienbrosRepositorio
    {
        Task InsertarMienbroxUsuario(IMienbro value);

        Task AsociarMienbroAHogar(IMienbroHogarAsociado request);
    }
}
