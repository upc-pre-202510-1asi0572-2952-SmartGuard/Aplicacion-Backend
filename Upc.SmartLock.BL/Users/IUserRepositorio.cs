using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BE.Usuario.Response;

namespace UPC.SmartLock.BL.Users
{
    public interface IUserRepositorio
    {
        Task InsertarUsuario(IUsuarioRequest value);
        Task<List<IUsuarioResponse>> GetUsuarios();
    }
}
