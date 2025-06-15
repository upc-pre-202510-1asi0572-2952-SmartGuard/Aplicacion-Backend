using UPC.SmartLock.BE.Usuario.Dto;
using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BE.Usuario.Response;

namespace UPC.SmartLock.BL.Users
{
    public interface IUserRepositorio
    {
        #region Metodos Sql
        Task InsertarUsuario(IUsuarioRequest value);
        //Task<List<IUsuarioResponse>> GetUsuarios();
        Task<IUsuarioResponse> BuscarUsuarioXNickname(string nickname);
        Task<IUsuarioResponse> BuscarUsuarioXId(string userId);
        Task<IUsuarioResponse> BuscarUsuarioXEmail(string email);
        Task<IPerfilUsuarioResponse> BuscarPerfilUsuarioXNickname(string nickname);
        Task<IPerfilUsuarioResponse> BuscarPerfilUsuarioXId(string userId);
        Task<IPerfilUsuarioResponse> ActualizarPerfilUsuario(IPerfilUsuarioRequest request);
        Task EliminarUsuarioPorId(String userId);
        Task ActualizarContrasenaUsuario(string userId, string nuevaContrasena);

        #endregion

        #region Metodos TableStorage
        Task InsertarUsuarioTs(IUsuario value);
        Task<IUsuario> ObtenerUsuarioTs(string idpartitionKey, string rowKey);
        Task SubirImagen(string blobNombre, Stream content);

        #endregion

    }
}
