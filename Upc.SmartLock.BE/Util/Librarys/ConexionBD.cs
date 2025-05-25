using System.Data;
using System.Data.Common;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public abstract class ConexionBD<TConexion, TComando, TParametro> : IConexionBD, IDisposable where TConexion : DbConnection where TComando : DbCommand where TParametro : DbParameter
    {
        protected bool Liberando { get; set; }

        public TConexion ConexionInterna { get; protected set; }

        public TComando ComandoInterno { get; protected set; }

        public DbTransaction TransaccionInterna { get; protected set; }

        //protected Func<DbDataReader, int, string> ObtenerConversorCsv(Type tipo)
        //{
        //    return tipo.Name.ToLower() switch
        //    {
        //        "string" => ConversorCsv.ConversorLectorCadena,
        //        "int32" => ConversorCsv.ConversorLectorInt32,
        //        "int64" => ConversorCsv.ConversorLectorInt64,
        //        "datetime" => ConversorCsv.ConversorLectorFecha,
        //        "bool" => ConversorCsv.ConversorLectorBool,
        //        "byte[]" => ConversorCsv.ConversorLectorBytes,
        //        _ => ConversorCsv.ConversorLectorDefault,
        //    };
        //}

        //protected virtual ConversorCsv.Configuracion ObtenerConfiguracionCsv()
        //{
        //    return new ConversorCsv.Configuracion(ObtenerConversorCsv);
        //}

        private void IniciarInterno(string texto, CommandType tipo)
        {
            ComandoInterno.CommandType = tipo;
            ComandoInterno.CommandText = texto;
            ComandoInterno.Parameters.Clear();
        }

        public abstract void CargarParametros();

        public void Iniciar(string texto, int tipo)
        {
            IniciarInterno(texto, (CommandType)tipo);
        }

        public void IniciarConsulta(string consulta)
        {
            IniciarInterno(consulta, CommandType.Text);
        }

        public void IniciarProcedimiento(string nombre)
        {
            IniciarInterno(nombre, CommandType.StoredProcedure);
        }

        public void NuevoParametro(string nombre, object valor)
        {
            // esto por el momento no usarlo no se por que no machea
            TParametro value = null;
            ComandoInterno.Parameters.Add(value);
        }

        public void NuevoParametro(string nombre, DbType tipo)
        {
            DbParameter dbParameter = ComandoInterno.CreateParameter();
            dbParameter.ParameterName = nombre;
            dbParameter.DbType = tipo;
            dbParameter.Direction = ParameterDirection.Output;
            ComandoInterno.Parameters.Add(dbParameter);
        }

        public T ObtenerParametro<T>(string nombre)
        {
            return (T)ComandoInterno.Parameters[nombre].Value;
        }

        public T ObtenerParametro<T>(int indice)
        {
            return (T)ComandoInterno.Parameters[indice].Value;
        }

        public int Ejecutar()
        {
            return ComandoInterno.ExecuteNonQuery();
        }

        public object EjecutarValor()
        {
            return ComandoInterno.ExecuteScalar();
        }

        public T EjecutarValor<T>()
        {
            return (T)EjecutarValor();
        }

        public object EjecutarValor(int indice)
        {
            using (DbDataReader dbDataReader = EjecutarLector())
            {
                if (dbDataReader.Read())
                {
                    return dbDataReader.GetValue(indice);
                }
            }

            return null;
        }

        public T EjecutarValor<T>(int indice)
        {
            using (DbDataReader dbDataReader = EjecutarLector())
            {
                if (dbDataReader.Read())
                {
                    return dbDataReader.GetFieldValue<T>(indice);
                }
            }

            return default(T);
        }

        public object EjecutarValor(string nombre)
        {
            using (DbDataReader dbDataReader = EjecutarLector())
            {
                if (dbDataReader.Read())
                {
                    return dbDataReader.GetValue(dbDataReader.GetOrdinal(nombre));
                }
            }

            return null;
        }

        public T EjecutarValor<T>(string nombre)
        {
            using (DbDataReader dbDataReader = EjecutarLector())
            {
                if (dbDataReader.Read())
                {
                    return dbDataReader.GetFieldValue<T>(dbDataReader.GetOrdinal(nombre));
                }
            }

            return default(T);
        }

        public DbDataReader EjecutarLector()
        {
            return ComandoInterno.ExecuteReader();
        }

        //public void EjecutarCsv(Stream flujo, string nombre)
        //{
        //    using DbDataReader lector = EjecutarLector();
        //    ConversorCsv.SerializarLector(flujo, ObtenerConfiguracionCsv(), lector, nombre);
        //}

        public void EjecutarBlob(Stream flujo, int indice, byte[] buffer)
        {
            int num = buffer.Length;
            int num2 = 0;
            using DbDataReader dbDataReader = EjecutarLector();
            if (dbDataReader.Read())
            {
                while (num == buffer.Length)
                {
                    num = (int)dbDataReader.GetBytes(indice, num2, buffer, 0, buffer.Length);
                    num2 += num;
                    flujo.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public async Task<int> EjecutarAsync()
        {
            return await ComandoInterno.ExecuteNonQueryAsync();
        }

        public async Task<object> EjecutarValorAsync()
        {
            return await ComandoInterno.ExecuteScalarAsync();
        }

        public async Task<T> EjecutarValorAsync<T>()
        {
            return (T)(await EjecutarValorAsync());
        }

        public async Task<object> EjecutarValorAsync(int indice)
        {
            using (DbDataReader reader = await EjecutarLectorAsync())
            {
                if (await reader.ReadAsync())
                {
                    return reader.GetValue(indice);
                }
            }

            return null;
        }

        public async Task<T> EjecutarValorAsync<T>(int indice)
        {
            using (DbDataReader reader = await EjecutarLectorAsync())
            {
                if (await reader.ReadAsync())
                {
                    return await reader.GetFieldValueAsync<T>(indice);
                }
            }

            return default(T);
        }

        public async Task<object> EjecutarValorAsync(string nombre)
        {
            using (DbDataReader reader = await EjecutarLectorAsync())
            {
                if (await reader.ReadAsync())
                {
                    return reader.GetValue(reader.GetOrdinal(nombre));
                }
            }

            return null;
        }

        public async Task<T> EjecutarValorAsync<T>(string nombre)
        {
            using (DbDataReader reader = await EjecutarLectorAsync())
            {
                if (await reader.ReadAsync())
                {
                    return await reader.GetFieldValueAsync<T>(reader.GetOrdinal(nombre));
                }
            }

            return default(T);
        }

        public async Task<DbDataReader> EjecutarLectorAsync()
        {
            return await ComandoInterno.ExecuteReaderAsync();
        }

        //public async Task EjecutarCsvAsync(Stream flujo, string nombre)
        //{
        //    using DbDataReader lector = await EjecutarLectorAsync();
        //    ConversorCsv.SerializarLector(flujo, ObtenerConfiguracionCsv(), lector, nombre);
        //}

        public async Task EjecutarBlobAsync(Stream flujo, int indice, byte[] buffer)
        {
            int bufferTamanio = buffer.Length;
            int bufferOffset = 0;
            using DbDataReader dbDataReader = await EjecutarLectorAsync();
            if (dbDataReader.Read())
            {
                while (bufferTamanio == buffer.Length)
                {
                    bufferTamanio = (int)dbDataReader.GetBytes(indice, bufferOffset, buffer, 0, buffer.Length);
                    bufferOffset += bufferTamanio;
                    flujo.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public void IniciarTransaccion()
        {
            if (TransaccionInterna == null)
            {
                TransaccionInterna = ConexionInterna.BeginTransaction();
                ComandoInterno.Transaction = TransaccionInterna;
            }
        }

        public void EjecutarTransaccion()
        {
            TransaccionInterna.Commit();
            TransaccionInterna.Dispose();
            ComandoInterno.Transaction = null;
            TransaccionInterna = null;
        }

        public void CancelarTransaccion()
        {
            TransaccionInterna.Rollback();
            TransaccionInterna.Dispose();
            ComandoInterno.Transaction = null;
            TransaccionInterna = null;
        }

        public async ValueTask IniciarTransaccionAsync()
        {
            if (TransaccionInterna == null)
            {
                TransaccionInterna = await ConexionInterna.BeginTransactionAsync();
            }
        }

        public async Task EjecutarTransaccionAsync()
        {
            await TransaccionInterna.CommitAsync();
            TransaccionInterna.Dispose();
            TransaccionInterna = null;
        }

        public async Task CancelarTransaccionAsync()
        {
            await TransaccionInterna.RollbackAsync();
            TransaccionInterna.Dispose();
            TransaccionInterna = null;
        }

        public void Dispose()
        {
            if (!Liberando)
            {
                Liberando = true;
                if (TransaccionInterna != null)
                {
                    TransaccionInterna.Rollback();
                }

                if (ConexionInterna != null)
                {
                    ConexionInterna.Close();
                }

                TransaccionInterna = null;
                ConexionInterna = null;
                ComandoInterno = null;
                GC.SuppressFinalize(this);
            }
        }

        public void EjecutarCsv(Stream flujo, string nombre)
        {
            throw new NotImplementedException();
        }

        public Task EjecutarCsvAsync(Stream flujo, string nombre)
        {
            throw new NotImplementedException();
        }

        public ConexionBD(string cadena, bool abrir = true)
        {
            Liberando = false;
            ConexionInterna = Activator.CreateInstance<TConexion>();
            ConexionInterna.ConnectionString = cadena;
            if (abrir)
            {
                ConexionInterna.Open();
            }

            ComandoInterno = Activator.CreateInstance<TComando>();
            ComandoInterno.Connection = ConexionInterna;
        }
    }
}
