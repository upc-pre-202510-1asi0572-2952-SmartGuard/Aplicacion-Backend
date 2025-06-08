using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SmartLock.BE.Hogar.Request
{
    public class  AsociarMiembroRequest : IAsociarMiembroRequest
    {
        public int HogarId { get; set; }
        public int UserId { get; set; }
    }
}
