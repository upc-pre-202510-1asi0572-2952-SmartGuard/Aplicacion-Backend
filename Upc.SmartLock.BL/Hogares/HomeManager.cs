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
                Nombre = request.Nombre,
                Direccion = request.Direccion,
                ImgUrl = request.ImgUrl,
                TipoPropiedad = request.TipoPropiedad,
                Habitaciones = request.Habitaciones,
                Baños = request.Baños,
                Calefaccion = request.Calefaccion,
                AbastecimientoAgua = request.AbastecimientoAgua,
                ProveedorInternet = request.ProveedorInternet,
                SistemaSeguridad = request.SistemaSeguridad,
                FuncionesInteligentes = request.FuncionesInteligentes,
                PropietarioId = usuarioAsociado.Result.Id,
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


        public async Task EliminarHogarPorId(String hogarId)
        {
            var hogarAsociado = await _homeRepositorio.GetHogarPorId(hogarId);
            if (hogarAsociado == null) { throw new MensajeException("Hogar No encontrado"); }

            await _homeRepositorio.EliminarHogarPorId(hogarId);
        }


        public async Task<IHogarResponse>ActualizarHogar(IHogarRequest request)
        {
            ValidarHogar(request);
            var hogarAsociado = await _homeRepositorio.GetHogarPorId(request.Id);
            if (hogarAsociado == null) { throw new MensajeException("Hogar No encontrado"); }

            var hogarUpdate = new Hogar
            {
                Id = hogarAsociado.Id,
                Nombre = request.Nombre,
                Direccion = request.Direccion,
                ImgUrl = request.ImgUrl,
                TipoPropiedad = request.TipoPropiedad,
                Habitaciones = request.Habitaciones,
                Baños = request.Baños,
                Calefaccion = request.Calefaccion,
                AbastecimientoAgua = request.AbastecimientoAgua,
                ProveedorInternet = request.ProveedorInternet,
                SistemaSeguridad = request.SistemaSeguridad,
                FuncionesInteligentes = request.FuncionesInteligentes,
                PropietarioId = hogarAsociado.PropietarioId
            };

            await _homeRepositorio.ActualizarHogar(hogarUpdate);
            return await _homeRepositorio.GetHogarPorId(request.Id);
        }

        public async Task<IHogarResponse> ObtenerHogarPorId(String hogarId)
        {
            var hogarAsociado = await _homeRepositorio.GetHogarPorId(hogarId);
            if (hogarAsociado == null) { throw new MensajeException("Hogar No encontrado"); }
            return hogarAsociado;
        }

        public async Task<List<IHogarResponse>> ObtenerHogaresPorPropietarioNickname(String Nickname)
        {
            var usuarioAsociado = _userRepositorio.BuscarUsuarioXNickname(Nickname);
            if (usuarioAsociado.Result == null) { throw new MensajeException("Usuario No encontrado"); }
            return await _homeRepositorio.GetHogaresPorPropietarioId(usuarioAsociado.Result.Id);
        }

       

        //public async Task<bool> ExisteMiembroEnHogar(IAsociarMiembroRequest request)
        //{
        //    ValidarMiembrosHogar(request);

        //    return await _homeRepositorio.ExisteMiembroEnHogar(request);
        //}

        //public async Task EliminarMiembroHogar(IAsociarMiembroRequest request)
        //{
        //    ValidarMiembrosHogar(request);

        //    await _homeRepositorio.EliminarMiembroHogar(request);
        //}




        //public async Task<List<IHogarMiembrosResponse>> ObtenerMiembrosHogar(String hogarId)
        //{
        //    return await _homeRepositorio.GetMiembrosAdmitidos(hogarId);
        //}

        //public async Task AsociarMiembroHogar(IAsociarMiembroRequest request)
        //{
        //    ValidarMiembrosHogar(request);
        //    await _homeRepositorio.InsertarMiembroHogar(request);
        //}



    }
}
