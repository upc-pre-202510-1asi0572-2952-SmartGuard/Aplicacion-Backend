using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SmartLock.BE.Usuario.Response;
using UPC.SmartLock.BE.Util;
using UPC.SmartLock.BL.Dipositivos;
using UPC.SmartLock.BL.Homes;
using UPC.SmartLock.BL.Users;
using UPC.SmartLock.BL.Util.Interface;

namespace UPC.SmartLock.BL.Auth
{
    public class AuthManager
    {
        private IUserRepositorio _userRepositorio = default(IUserRepositorio);
        private IAESEncriptacion _encriptacionService;

        public AuthManager(Repositorio repo, IAESEncriptacion encriptacionService)
        {
            _userRepositorio = new UserRepositorio(repo);
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


        public async Task<IUsuarioResponse> Login(string email, string password)
        {
            string contraseniaHasheada = Encriptar(password);
            var usuarioBD = await _userRepositorio.BuscarUsuarioXEmail(email);
            if(usuarioBD == null) { throw new MensajeExceptionExtendido("Usuario No Encontrado"); }
            if (usuarioBD != null && usuarioBD.Contrasenia != contraseniaHasheada) { throw new MensajeExceptionExtendido("Contraseña equivocada"); }

            return usuarioBD;
        }


    }
}
