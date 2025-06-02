namespace UPC.SmartLock.BE.Util.Librarys
{
    public class Almacenamiento : AlmacenamientoBase
    {
        public static IAlmacenamiento Instancia { get; set; }

        public static string LLAVE
        {
            get
            {
                if (Instancia != null)
                {
                    return Instancia.Llave;
                }

                return null;
            }
            set
            {
                if (Instancia == null)
                {
                    Instancia = new Almacenamiento();
                }

                Instancia.Llave = value;
            }
        }

        public static string NOMBRE
        {
            get
            {
                if (Instancia != null)
                {
                    return Instancia.Nombre;
                }

                return null;
            }
            set
            {
                if (Instancia == null)
                {
                    Instancia = new Almacenamiento();
                }

                Instancia.Nombre = value;
            }
        }
    }
}
