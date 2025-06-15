using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SmartLock.BE.Usuario.Request
{
    public class ActualizarContrasenaRequest : IActualizarContrasenaRequest
    {
        public string Id { get; set; }
        public string ContrasenaActual { get; set; }
        public string NuevaContrasena { get; set; }
        public string ConfirmacionContrasena { get; set; }
    }
}
