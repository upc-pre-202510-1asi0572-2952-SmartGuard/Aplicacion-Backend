namespace UPC.SmartLock.BE.Aplicacion
{
    public class TokenSesion
    {
        public long Ruc { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public byte[] ObtenerBuffer()
        {
            var buffer = new List<byte>();

            buffer.AddRange(BitConverter.GetBytes(Ruc));
            buffer.AddRange(BitConverter.GetBytes(FechaInicio.Ticks));
            buffer.AddRange(BitConverter.GetBytes(FechaFin.Ticks));

            return buffer.ToArray();
        }
        public void CargarBuffer(byte[] buffer)
        {
            Ruc = BitConverter.ToInt64(buffer, 0);
            FechaInicio = new DateTime(BitConverter.ToInt64(buffer, 8));
            FechaFin = new DateTime(BitConverter.ToInt64(buffer, 16));
        }
    }
}
