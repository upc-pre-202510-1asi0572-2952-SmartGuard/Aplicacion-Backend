using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public class ConexionMysql : ConexionBD<MySqlConnection, MySqlCommand, MySqlParameter>
    {
        protected string DeSnakeCase(string valor)
        {
            List<char> list = new List<char>(valor);
            int index = 0;
            int count = list.Count;
            int num = 0;
            num = list[index];
            if (num > 96 && num < 123)
            {
                list[index] = (char)(num - 32);
            }

            for (index = 1; index < count; index++)
            {
                num = list[index];
                if (num == 95)
                {
                    list.RemoveAt(index);
                    index = Math.Min(index, count - 1);
                    num = list[index];
                    if (num > 96 && num < 123)
                    {
                        list[index] = (char)(num - 32);
                    }

                    count = list.Count;
                }
            }

            return new string(list.ToArray());
        }

        // aca me quedo
        protected Func<DbDataReader, int, string> ObtenerConversorBulk(string nombre)
        {
            switch (nombre.ToLower())
            {
                case "varchar":
                case "char":
                case "nvarchar":
                case "nchar":
                    return ConversorBulk.ConversorLectorCadena;
                case "time":
                    return ConversorBulk.ConversorLectorHora;
                case "date":
                    return ConversorBulk.ConversorLectorFecha;
                case "datetime":
                    return ConversorBulk.ConversorLectorFechaHora;
                default:
                    return ConversorBulk.ConversorLectorDefault;
            }
        }

        protected ConversorBulk.Configuracion ObtenerConfiguracionBulk()
        {
            return new ConversorBulk.Configuracion(ObtenerConversorBulk);
        }

        //protected override ConversorCsv.Configuracion ObtenerConfiguracionCsv()
        //{
        //    return new ConversorCsv.Configuracion(base.ObtenerConversorCsv, DeSnakeCase);
        //}

        public override void CargarParametros()
        {
            MySqlCommandBuilder.DeriveParameters(base.ComandoInterno);
        }

        public void NuevoParametro(string nombre, MySqlDbType tipo)
        {
            MySqlParameter mySqlParameter = base.ComandoInterno.CreateParameter();
            mySqlParameter.ParameterName = nombre;
            mySqlParameter.MySqlDbType = tipo;
            mySqlParameter.Direction = ParameterDirection.Output;
            base.ComandoInterno.Parameters.Add(mySqlParameter);
        }

        public void NuevoParametro(string nombre, MySqlDbType tipo, int size)
        {
            MySqlParameter mySqlParameter = base.ComandoInterno.CreateParameter();
            mySqlParameter.ParameterName = nombre;
            mySqlParameter.MySqlDbType = tipo;
            mySqlParameter.Direction = ParameterDirection.Output;
            mySqlParameter.Size = size;
            base.ComandoInterno.Parameters.Add(mySqlParameter);
        }

        public void EjecutarBulk(Stream flujo)
        {
            using DbDataReader lector = EjecutarLector();
            ConversorBulk.SerializarLector(flujo, ObtenerConfiguracionBulk(), lector);
        }

        public async Task EjecutarBulkAsync(Stream flujo)
        {
            using DbDataReader lector = await EjecutarLectorAsync();
            await ConversorBulk.SerializarLectorAsync(flujo, ObtenerConfiguracionBulk(), lector);
        }

        public ConexionMysql(string cadena, bool abrir = true)
            : base(cadena, abrir)
        {
        }
    }
}
