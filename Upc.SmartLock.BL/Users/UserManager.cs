using UPC.SmartLock.BE.Dispositivos.Response;
using UPC.SmartLock.BE.Usuario.Dto;
using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BE.Usuario.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.BL.Dipositivos;
using UPC.SmartLock.BL.Util;
using UPC.SmartLock.BL.Util.Interface;

namespace UPC.SmartLock.BL.Users
{
    public class UserManager
    {
        private IUserRepositorio _userRepositorio = default(IUserRepositorio);
        private IDipositivoRepositorio _dispositivoRepositorio = default(IDipositivoRepositorio);
        private IAESEncriptacion _encriptacionService;

        public UserManager(Repositorio repo, IAESEncriptacion encriptacionService)
        {
            _userRepositorio = new UserRepositorio(repo);
            _dispositivoRepositorio = new DipositivoRepositorio(repo);
            _encriptacionService = encriptacionService;

        }

        public string Encriptar(string valor)
        {
            string cadenaEncriptada = string.Empty;
            cadenaEncriptada = _encriptacionService.EncriptarCadena(valor);
            return cadenaEncriptada;
        }

        public string Desencriptar(string valor)
        {
            if (valor == null) { return "No hay data en este campo para este ruc"; }
            string cadenaDesencriptada = string.Empty;
            cadenaDesencriptada = _encriptacionService.DesencriptarCadena(valor);
            return cadenaDesencriptada;
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

        public async Task<IUsuarioResponse> CrearUsuario(IUsuarioRequest request)
        {
            ValidarUsuario(request);
         
            request.Id = GeneradorGuid.NuevoGuid();
            request.Contrasenia = Encriptar(request.Contrasenia);
            await _userRepositorio.InsertarUsuario(request);

            return await _userRepositorio.BuscarUsuarioXId(request.Id);
        }

        public async Task ActualizarContrasena(IActualizarContrasenaRequest request)
        {
            var user = await _userRepositorio.BuscarUsuarioXId(request.Id);
            if (user == null)
                throw new MensajeException("Usuario no encontrado");

            var contraseniaActualEncriptada = Encriptar(request.ContrasenaActual);
            if (user.Contrasenia != contraseniaActualEncriptada)
                throw new MensajeException("La contraseña actual no es correcta");

            if (request.NuevaContrasena != request.ConfirmacionContrasena)
                throw new MensajeException("La nueva contraseña y la confirmación no coinciden");

            var nuevaEncriptada = Encriptar(request.NuevaContrasena);
            await _userRepositorio.ActualizarContrasenaUsuario(request.Id, nuevaEncriptada);
        }


        public async Task<IPerfilUsuarioResponse> ActualizarPerfilUsuario(IPerfilUsuarioRequest request)
        {
            var user = await _userRepositorio.BuscarUsuarioXId(request.Id);
            if (user == null) { throw new MensajeException("Usuario con dicho id no existe"); }
            return await _userRepositorio.ActualizarPerfilUsuario(request);
        }


        public async Task EliminarUsuarioPorId(String userId)
        {
            var usuarioAsociado = await _userRepositorio.BuscarPerfilUsuarioXId(userId);
            if (usuarioAsociado == null) { throw new MensajeException("Usuario No encontrado"); }

            await _userRepositorio.EliminarUsuarioPorId(userId);
        }




        public async Task<List<DispositivoResponse>> obtenerDispositivosXUsuario(string nickname)
        {
            var userBd = await _userRepositorio.BuscarUsuarioXNickname(nickname);
            if (userBd == null) { throw new MensajeException("Usuario con dicho nickname no existe"); }
            return await _dispositivoRepositorio.ObtenerDispositivosXUsuario(nickname);
        }

        public async Task<IUsuario> ObtenerUsuarioTS(string partitionKey, string rowKey)
        {
            return await _userRepositorio.ObtenerUsuarioTs(partitionKey, rowKey);
        }

        public async Task<IPerfilUsuarioResponse> ObtenerPerfilUsuarioPorNickname(string nickname)
        {
            var user = await _userRepositorio.BuscarUsuarioXNickname(nickname);
            if (user == null) { throw new MensajeException("Usuario con dicho nickname no existe"); }
            return await _userRepositorio.BuscarPerfilUsuarioXNickname(nickname);
        }




        public async Task SubirImagenUsuarioTS(string nombreBlob, string imagenBase64)
        {
            Stream imagen;
            var fileName = default(string);

            byte[] imgBytes = Convert.FromBase64String(imagenBase64);
            imagen = new MemoryStream(imgBytes);


            string extension = GetImageMimeType(imgBytes);

            await _userRepositorio.SubirImagen($"{nombreBlob}{extension}", imagen);
        }

        string GetImageMimeType(byte[] imageBytes)
        {
            // JPG
            if (imageBytes.Length > 3 && imageBytes[0] == 0xFF && imageBytes[1] == 0xD8)
                return ".jpeg";

            // PNG
            if (imageBytes.Length > 8 &&
                imageBytes[0] == 0x89 &&
                imageBytes[1] == 0x50 &&
                imageBytes[2] == 0x4E &&
                imageBytes[3] == 0x47)
                return ".png";

            // GIF
            if (imageBytes.Length > 6 &&
                imageBytes[0] == 0x47 &&
                imageBytes[1] == 0x49 &&
                imageBytes[2] == 0x46)
                return ".gif";

            // BMP
            if (imageBytes.Length > 2 && imageBytes[0] == 0x42 && imageBytes[1] == 0x4D)
                return ".bmp";

            return "unknown";
        }
    }
}
