﻿namespace UPC.SmartLock.BE.Usuario.Response
{
    public class UsuarioResponse : IUsuarioResponse
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }
}
