using UPC.SmartLock.BE.Mienbros.Dto;
using UPC.SmartLock.BE.Mienbros.Request;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.BL.Homes;
using UPC.SmartLock.BL.Users;
using UPC.SmartLock.BL.Util;

namespace UPC.SmartLock.BL.Miembros
{
    public class MienbroManager
    {
        private IUserRepositorio _userRepositorio = default(IUserRepositorio);
        private IHomeRepositorio _homeRepositorio = default(IHomeRepositorio);
        private IMienbrosRepositorio _mienbroRepositorio = default(IMienbrosRepositorio);
        public MienbroManager(Repositorio repo)
        {
            _userRepositorio = new UserRepositorio(repo);
            _homeRepositorio = new HomeRepositorio(repo);
            _mienbroRepositorio = new MienbrosRepositorio(repo);
        }
        public async Task CrearNuevoMienbro(IMienbroRequest request)
        {
            var usuarioAsociado = _userRepositorio.BuscarUsuarioXNickname(request.userNickname);
            if (usuarioAsociado.Result == null) { throw new MensajeException("Usuario No encontrado"); }

            var value = new Mienbro
            {
                Id = GeneradorGuid.NuevoGuid(),
                Nombre = request.Nombre,
                Edad = request.Edad,
                Parentesco = request.Parentesco,
                Descripcion = request.Descripcion,
                FotoPerfil = request.FotoPerfil,
                UserId = usuarioAsociado.Result.Id,
                //HogarId = string.IsNullOrWhiteSpace(request.HogarId) ? null : request.HogarId,
            };
            await _mienbroRepositorio.InsertarMienbroxUsuario(value);
        }

        public async Task AsociarMienbroAHogar(IAsociarMienbroRequest request)
        {
            var asociacion = new MienbroHogarAsociado
            {
                Id = GeneradorGuid.NuevoGuid(),
                HogarId = request.HogarId,
                MienbroId = request.MienbroId,
                Estatus = request.Estatus
            };
            await _mienbroRepositorio.AsociarMienbroAHogar(asociacion);
        }


    }
}
