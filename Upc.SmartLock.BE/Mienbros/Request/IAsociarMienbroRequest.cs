using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SmartLock.BE.Mienbros.Request
{
    public interface IAsociarMienbroRequest
    {
        public string HogarId { get; set; }
        public string MienbroId { get; set; }
        public bool Estatus { get; set; }   
    }
}
