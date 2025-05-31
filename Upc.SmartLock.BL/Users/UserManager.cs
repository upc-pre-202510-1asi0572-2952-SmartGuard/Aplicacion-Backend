using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BE.Usuario.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;

namespace UPC.SmartLock.BL.Users
{
    public class UserManager
    {
        private IUserRepositorio _userRepositorio = default(IUserRepositorio);

        public UserManager(Repositorio repo)
        {
            _userRepositorio = new UserRepositorio(repo);
        }

        public void ValidarUsuario(IUsuarioRequest request)
        {
            var validator = new UserValidator();
            var validacion = validator.Validate(request);

            if (!validacion.IsValid)
            {
                throw new MensajeException(string.Join(" ", validacion.Errors.Select(e => e.ErrorMessage)));
            }
        }

        public async Task CrearUsuario(IUsuarioRequest request)
        {
            ValidarUsuario(request);
            await _userRepositorio.InsertarUsuario(request);
        }

        public async Task<List<IUsuarioResponse>> ObtenerUsuario()
        {
            return await _userRepositorio.GetUsuarios();
        }
    }
}
