using UPC.SmartLock.BE.Usuario.Dto;
using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BE.Usuario.Response;

namespace UPC.SmartLock.BL.Users
{
    public interface IUserRepositorio
    {
        #region Metodos Sql
        Task InsertarUsuario(IUsuarioRequest value);
        Task<List<IUsuarioResponse>> GetUsuarios();
        #endregion


        #region Metodos TableStorage
        Task InsertarUsuarioTs(IUsuario value);
        Task<IUsuario> ObtenerUsuario(string idpartitionKey, string rowKey);
        Task SubirImagen(string blobNombre, Stream content);
        #endregion

    }
}
