using Microsoft.OData.UriParser;
using System.Text.RegularExpressions;
using UPC.SmartLock.BE.Hogar.Response;
using UPC.SmartLock.BE.Usuario.Dto;
using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BE.Usuario.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.BL.Util;

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

            //var usuario = new UsuarioRequest();
            request.Id = GeneradorGuid.NuevoGuid();

            await _userRepositorio.InsertarUsuario(request);
        }

        //public async Task<List<IUsuarioResponse>> ObtenerUsuarios()
        //{
        //    return await _userRepositorio.GetUsuarios();
        //}

        //public async Task<IUsuarioResponse> ObtenerUsuarioPorId(int usuarioId)
        //{
        //    return await _userRepositorio.GetUsuarioPorId(usuarioId);
        //}



        //public async Task CrearUsuarioTs(IUsuario value)
        //{
        //    value.Id = GeneradorGuid.NuevoId();
        //    await _userRepositorio.InsertarUsuarioTs(value);
        //}

        public async Task<IUsuario> ObtenerUsuarioTS(string partitionKey, string rowKey)
        {
            return await _userRepositorio.ObtenerUsuarioTs(partitionKey, rowKey);
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
