using Microsoft.Azure.Cosmos.Table;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public abstract class TableStorageAcceso<TInterface, TImplementation> where TImplementation : TableEntity, TInterface, new()
    {
        public const string ConsultaEqual = "eq";

        public const string ConsultaGreaterThan = "gt";

        public const string ConsultaGreaterThanOrEqual = "ge";

        public const string ConsultaLessThan = "lt";

        public const string ConsultaLessThanOrEqual = "le";

        public const string ConsultaNotEqual = "ne";

        public const string ConsultaAnd = "and";

        public const string ConsultaOr = "or";

        public const string ConsultaNot = "not";

        protected CloudTableClient Cliente { get; set; }

        protected CloudTable Tabla { get; set; }

        protected virtual string Nombre { get; }

        protected abstract TImplementation CrearObjeto(TInterface valor);

        protected async ValueTask<List<TInterface>> ListarPorConsultaAsync(TableQuery<TImplementation> consulta)
        {
            List<TInterface> result = new List<TInterface>();
            TableContinuationToken tableContinuationToken = null;
            do
            {
                TableQuerySegment<TImplementation> tableQuerySegment = await Tabla.ExecuteQuerySegmentedAsync(consulta, tableContinuationToken);
                result.AddRange(tableQuerySegment.Results);
                tableContinuationToken = tableQuerySegment.ContinuationToken;
            }
            while (tableContinuationToken != null);
            return result;
        }

        protected async ValueTask<List<TInterface>> ListarPorConsultaAsync(string texto)
        {
            return await ListarPorConsultaAsync(new TableQuery<TImplementation>().Where(texto));
        }

        protected TImplementation CrearObjetoETag(TInterface valor)
        {
            TImplementation val = CrearObjeto(valor);
            val.ETag = "*";
            return val;
        }

        public async ValueTask InsertarAsync(TInterface valor)
        {
            await Tabla.ExecuteAsync(TableOperation.InsertOrReplace(CrearObjeto(valor)));
        }

        public async ValueTask InsertarEstrictoAsync(TInterface valor)
        {
            await Tabla.ExecuteAsync(TableOperation.Insert(CrearObjeto(valor)));
        }

        public async ValueTask ActualizarAsync(TInterface valor)
        {
            await Tabla.ExecuteAsync(TableOperation.InsertOrMerge(CrearObjeto(valor)));
        }

        public async ValueTask ActualizarEstrictoAsync(TInterface valor)
        {
            await Tabla.ExecuteAsync(TableOperation.Merge(CrearObjetoETag(valor)));
        }

        public async ValueTask EliminarAsync(TInterface valor)
        {
            await Tabla.ExecuteAsync(TableOperation.Delete(CrearObjetoETag(valor)));
        }

        public async ValueTask<List<TInterface>> ListarAsync()
        {
            return await ListarPorConsultaAsync(new TableQuery<TImplementation>());
        }

        public async ValueTask InsertarBatchAsync(IEnumerable<TInterface> valores)
        {
            await Tabla.EjecutarBatchAsync(valores.Select((TInterface x) => TableOperation.InsertOrReplace(CrearObjeto(x))).ToList());
        }

        public async ValueTask InsertarBatchEstrictoAsync(IEnumerable<TInterface> valores)
        {
            await Tabla.EjecutarBatchAsync(valores.Select((TInterface x) => TableOperation.Insert(CrearObjeto(x))).ToList());
        }

        public async ValueTask ActualizarBatchAsync(IEnumerable<TInterface> valores)
        {
            await Tabla.EjecutarBatchAsync(valores.Select((TInterface x) => TableOperation.InsertOrMerge(CrearObjeto(x))).ToList());
        }

        public async ValueTask ActualizarBatchEstrictoAsync(IEnumerable<TInterface> valores)
        {
            await Tabla.EjecutarBatchAsync(valores.Select((TInterface x) => TableOperation.Merge(CrearObjetoETag(x))).ToList());
        }

        public async ValueTask EliminarBatchAsync(IEnumerable<TInterface> valores)
        {
            await Tabla.EjecutarBatchAsync(valores.Select((TInterface x) => TableOperation.Delete(CrearObjetoETag(x))).ToList());
        }

        public void InsertarParalelo(IEnumerable<TInterface> valores)
        {
            Tabla.EjecutarParalelo(valores.Select((TInterface x) => TableOperation.InsertOrReplace(CrearObjeto(x))).ToList());
        }

        public void InsertarParaleloEstricto(IEnumerable<TInterface> valores)
        {
            Tabla.EjecutarParalelo(valores.Select((TInterface x) => TableOperation.Insert(CrearObjeto(x))).ToList());
        }

        public void ActualizarParalelo(IEnumerable<TInterface> valores)
        {
            Tabla.EjecutarParalelo(valores.Select((TInterface x) => TableOperation.InsertOrMerge(CrearObjeto(x))).ToList());
        }

        public void ActualizarParaleloEstricto(IEnumerable<TInterface> valores)
        {
            Tabla.EjecutarParalelo(valores.Select((TInterface x) => TableOperation.Merge(CrearObjetoETag(x))).ToList());
        }

        public void EliminarParalelo(IEnumerable<TInterface> valores)
        {
            Tabla.EjecutarParalelo(valores.Select((TInterface x) => TableOperation.Delete(CrearObjetoETag(x))).ToList());
        }

        public async ValueTask<bool> CrearTablaSiNoExisteAsync()
        {
            return await Tabla.CreateIfNotExistsAsync();
        }

        public static string ConsultaCombinar(string filtroA, string operatorString, string filtroB)
        {
            return TableQuery.CombineFilters(filtroA, operatorString, filtroB);
        }

        public static string ConsultaFiltro(string propiedad, string operacion, string valor)
        {
            return TableQuery.GenerateFilterCondition(propiedad, operacion, valor);
        }

        public static string ConsultaFiltro(string propiedad, string operacion, byte[] valor)
        {
            return TableQuery.GenerateFilterConditionForBinary(propiedad, operacion, valor);
        }

        public static string ConsultaFiltro(string propiedad, string operacion, bool valor)
        {
            return TableQuery.GenerateFilterConditionForBool(propiedad, operacion, valor);
        }

        public static string ConsultaFiltro(string propiedad, string operacion, DateTimeOffset valor)
        {
            return TableQuery.GenerateFilterConditionForDate(propiedad, operacion, valor);
        }

        public static string ConsultaFiltro(string propiedad, string operacion, double valor)
        {
            return TableQuery.GenerateFilterConditionForDouble(propiedad, operacion, valor);
        }

        public static string ConsultaFiltro(string propiedad, string operacion, Guid valor)
        {
            return TableQuery.GenerateFilterConditionForGuid(propiedad, operacion, valor);
        }

        public static string ConsultaFiltro(string propiedad, string operacion, int valor)
        {
            return TableQuery.GenerateFilterConditionForInt(propiedad, operacion, valor);
        }

        public static string ConsultaFiltro(string propiedad, string operacion, long valor)
        {
            return TableQuery.GenerateFilterConditionForLong(propiedad, operacion, valor);
        }

        public TableStorageAcceso()
        {
            Tabla = Almacenamiento.Instancia.ObtenerClienteTabla().GetTableReference(Nombre);
        }

        public TableStorageAcceso(IAlmacenamiento almacenamiento)
        {
            Tabla = almacenamiento.ObtenerClienteTabla().GetTableReference(Nombre);
        }

        public TableStorageAcceso(CloudTableClient cliente, string nombre)
        {
            Tabla = cliente.GetTableReference(nombre);
        }

        public TableStorageAcceso(CloudTableClient cliente)
        {
            Tabla = cliente.GetTableReference(Nombre);
        }
    }
}
