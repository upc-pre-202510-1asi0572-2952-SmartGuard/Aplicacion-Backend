using Microsoft.Azure.Cosmos;
using System.Security.Cryptography;
using UPC.SmartLock.BE.Dispositivos.Dto;
using UPC.SmartLock.BE.Hogar.Dto;
using UPC.SmartLock.BE.Hogar.Request;
using UPC.SmartLock.BE.Hogar.Response;
using UPC.SmartLock.BE.Usuario.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.BL.Dipositivos;
using UPC.SmartLock.BL.Users;
using UPC.SmartLock.BL.Util;

namespace UPC.SmartLock.BL.Homes
{
    public class HomeManager
    {
        private IHomeRepositorio _homeRepositorio = default(IHomeRepositorio);
        private IUserRepositorio _userRepositorio = default(IUserRepositorio);
        private IDipositivoRepositorio _dispositivoRepositorio = default(IDipositivoRepositorio);
        public HomeManager(Repositorio repo)
        {
            _homeRepositorio = new HomeRepositorio(repo);
            _userRepositorio = new UserRepositorio(repo);
            _dispositivoRepositorio = new DipositivoRepositorio(repo);
        }

        public void ValidarHogar(IHogarRequest request)
        {
            var validator = new HomeValidator();
            var validacion = validator.Validate(request);

            if (!validacion.IsValid)
            {
                throw new MensajeException(string.Join(" ", validacion.Errors.Select(e => e.ErrorMessage)));
            }
        }


        public void ValidarMiembrosHogar(IAsociarMiembroRequest request)
        {
            var validator = new HomeMembersValidator();
            var validacion = validator.Validate(request);

            if (!validacion.IsValid)
            {
                throw new MensajeException(string.Join(" ", validacion.Errors.Select(e => e.ErrorMessage)));
            }
        }


        public async Task CrearHogar(IHogarRequest request)
        {
            ValidarHogar(request);
            var usuarioAsociado = _userRepositorio.BuscarUsuarioXNickname(request.Nickname);
            if(usuarioAsociado.Result == null) { throw new MensajeException("Usuario No encontrado"); }
            var hogar = new Hogar
            {
                Id = GeneradorGuid.NuevoGuid(),
                Direccion = request.Direccion,
                Nombre = request.Nombre,
                PropietarioId = usuarioAsociado.Result.Id,
                ImgUrl = request.ImgUrl
            };
            await _homeRepositorio.InsertarHogar(hogar);

            var dispositivo = new Dispositivo
            {
                Id = GeneradorGuid.NuevoGuid(),
                HogarId = hogar.Id,
                Serie = "ESP32-CAM",
                Modelo = "UTFD-1213",
                Porcentaje = 98,
                Puerta = false,
                Firmware = "2.34"
            };
            await _dispositivoRepositorio.InsertarDispositivo(dispositivo);
        }
        
        public async Task<List<IHogarResponse>> ObtenerHogaresPorPropietarioId(int propietarioId)
        {
            return await _homeRepositorio.GetHogaresPorPropietarioId(propietarioId);
        }

        public async Task CrearHogarTs(IHogar value)
        {
            value.Id = GeneradorGuid.NuevoGuid();
            await _homeRepositorio.InsertarHogarTs(value);
        }


        public async Task<IHogarResponse> ObtenerHogarPorId(int hogarId)
        {
            return await _homeRepositorio.GetHogarPorId(hogarId);
        }

        public async Task<bool> ExisteMiembroEnHogar(IAsociarMiembroRequest request)
        {
            ValidarMiembrosHogar(request);

            return await _homeRepositorio.ExisteMiembroEnHogar(request);
        }

        public async Task EliminarMiembroHogar(IAsociarMiembroRequest request)
        {
            ValidarMiembrosHogar(request);

            await _homeRepositorio.EliminarMiembroHogar(request);
        }




        public async Task<List<IHogarMiembrosResponse>> ObtenerMiembrosHogar(int hogarId)
        {
            return await _homeRepositorio.GetMiembrosAdmitidos(hogarId);
        }

        public async Task AsociarMiembroHogar(IAsociarMiembroRequest request)
        {
            ValidarMiembrosHogar(request);
            await _homeRepositorio.InsertarMiembroHogar(request);
        }



    }
}
