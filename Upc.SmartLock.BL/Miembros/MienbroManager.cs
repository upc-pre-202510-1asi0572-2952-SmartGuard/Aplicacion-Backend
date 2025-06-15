using Org.BouncyCastle.Bcpg.OpenPgp;
using UPC.SmartLock.BE.Hogar.Dto;
using UPC.SmartLock.BE.Hogar.Request;
using UPC.SmartLock.BE.Hogar.Response;
using UPC.SmartLock.BE.Mienbros.Dto;
using UPC.SmartLock.BE.Mienbros.Request;
using UPC.SmartLock.BE.Mienbros.Response;
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

        public async Task<MienbroInfoTemporalResponse> ObtenerInforMienbroTemporal(string idMienbro)
        {
            return await _mienbroRepositorio.ObtenerInfoMienbroTemporal(idMienbro);
        }

        public async Task<List<IMienbroResponse>> ObtenerMiembrosPorPropietarioNickname(String Nickname)
        {
            var usuarioAsociado = _userRepositorio.BuscarUsuarioXNickname(Nickname);
            if (usuarioAsociado.Result == null) { throw new MensajeException("Usuario No encontrado"); }
            return await _mienbroRepositorio.GetMiembrosPorPropietarioId(usuarioAsociado.Result.Id);
        }

        public async Task<List<IMienbroResponse>> ObtenerMiembrosHabilitadosPorHogarId(String HogarId)
        {
            var hogarAsociado = _homeRepositorio.GetHogarPorId(HogarId);
            if (hogarAsociado.Result == null) { throw new MensajeException("Hogar No encontrado"); }
            return await _mienbroRepositorio.GetMiembrosHabilitadosPorHogarId(HogarId);
        }



        public async Task<IMienbroResponse> ActualizarMiembro(IMienbroRequest request)
        {
            var miembroAsociado = await _mienbroRepositorio.GetMiembroPorId(request.Id);
            if (miembroAsociado == null) { throw new MensajeException("Mienbro No encontrado"); }

            var miembroUpdate = new Mienbro
            {
                Id = miembroAsociado.Id,
                Nombre = request.Nombre,
                Edad = request.Edad,
                Parentesco = request.Parentesco,
                Descripcion = request.Descripcion,
                FotoPerfil = request.FotoPerfil,
                UserId = miembroAsociado.UserId
            };

            await _mienbroRepositorio.ActualizarMiembro(miembroUpdate);
            return await _mienbroRepositorio.GetMiembroPorId(request.Id);
        }


        public async Task<IMienbroResponse> ObtenerMiembroPorId(String miembroId)
        {
            var miembroAsociado = await _mienbroRepositorio.GetMiembroPorId(miembroId);
            if (miembroAsociado == null) { throw new MensajeException("Mienbro No encontrado"); }
            return miembroAsociado;
        }

        public async Task EliminarMiembroPorId(String miembroId)
        {
            var miembroAsociado = await _mienbroRepositorio.GetMiembroPorId(miembroId);
            if (miembroAsociado == null) { throw new MensajeException("Mienbro No encontrado"); }

            await _mienbroRepositorio.EliminarMiembroPorId(miembroId);
        }





    }
}
