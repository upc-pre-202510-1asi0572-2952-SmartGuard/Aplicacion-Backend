using UPC.SmartLock.BE.Hogar.Dto;
using UPC.SmartLock.BE.Hogar.Request;
using UPC.SmartLock.BE.Hogar.Response;
using UPC.SmartLock.BE.Usuario.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BE.Util.Librarys;
using UPC.SmartLock.BL.Util;

namespace UPC.SmartLock.BL.Homes
{
    public class HomeManager
    {
        private IHomeRepositorio _HomeRepositorio = default(IHomeRepositorio);

        public HomeManager(Repositorio repo)
        {
            _HomeRepositorio = new HomeRepositorio(repo);
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
            await _HomeRepositorio.InsertarHogar(request);
        }
        public async Task ActualizarHogar(IHogarRequest request)
        {
            ValidarHogar(request);
            await _HomeRepositorio.ActualizarHogar(request);
        }

        public async Task<List<IHogarResponse>> ObtenerHogaresPorPropietarioId(int propietarioId)
        {
            return await _HomeRepositorio.GetHogaresPorPropietarioId(propietarioId);
        }

        public async Task CrearHogarTs(IHogar value)
        {
            value.Id = GeneradorGuid.NuevoId();
            await _HomeRepositorio.InsertarHogarTs(value);
        }


        public async Task<IHogarResponse> ObtenerHogarPorId(int hogarId)
        {
            return await _HomeRepositorio.GetHogarPorId(hogarId);
        }

        public async Task<bool> ExisteMiembroEnHogar(IAsociarMiembroRequest request)
        {
            ValidarMiembrosHogar(request);

            return await _HomeRepositorio.ExisteMiembroEnHogar(request);
        }

        public async Task EliminarMiembroHogar(IAsociarMiembroRequest request)
        {
            ValidarMiembrosHogar(request);

            await _HomeRepositorio.EliminarMiembroHogar(request);
        }




        public async Task<List<IHogarMiembrosResponse>> ObtenerMiembrosHogar(int hogarId)
        {
            return await _HomeRepositorio.GetMiembrosAdmitidos(hogarId);
        }

        public async Task AsociarMiembroHogar(IAsociarMiembroRequest request)
        {
            ValidarMiembrosHogar(request);
            await _HomeRepositorio.InsertarMiembroHogar(request);
        }



    }
}
