using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SmartLock.BE.Hogar.Response
{
    public class HogarMiembrosResponse : IHogarMiembrosResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Mote { get; set; }
    }
}
