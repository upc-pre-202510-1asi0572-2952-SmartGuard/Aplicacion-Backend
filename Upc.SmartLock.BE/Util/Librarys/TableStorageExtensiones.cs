using Microsoft.Azure.Cosmos.Table;

namespace UPC.SmartLock.BE.Util.Librarys
{
    public static class TableStorageExtensiones
    {
        public static void EjecutarParalelo(this CloudTable tabla, List<TableOperation> operaciones)
        {
            TableBatchOperation tableBatchOperation = new TableBatchOperation();
            string text = null;
            TableOperation tableOperation = null;
            Action<TableBatchOperation> body = delegate (TableBatchOperation x)
            {
                tabla.ExecuteBatchAsync(x).GetAwaiter().GetResult();
            };
            List<TableBatchOperation> list = new List<TableBatchOperation>();
            operaciones = operaciones.OrderBy((TableOperation x) => x.Entity.PartitionKey).ToList();
            tableOperation = operaciones[0];
            text = tableOperation.Entity.PartitionKey;
            tableBatchOperation.Add(tableOperation);
            for (int i = 1; i < operaciones.Count; i++)
            {
                tableOperation = operaciones[i];
                if (object.Equals(text, tableOperation.Entity.PartitionKey) && tableBatchOperation.Count < 100)
                {
                    tableBatchOperation.Add(tableOperation);
                    continue;
                }

                if (tableBatchOperation.Count > 0)
                {
                    list.Add(tableBatchOperation);
                }

                tableBatchOperation = new TableBatchOperation();
            }

            if (tableBatchOperation.Count > 0)
            {
                list.Add(tableBatchOperation);
            }

            if (list.Count > 0)
            {
                Parallel.ForEach((IEnumerable<TableBatchOperation>)list, body);
            }
        }

        public static async ValueTask EjecutarBatchAsync(this CloudTable tabla, List<TableOperation> operaciones)
        {
            TableBatchOperation batch = new TableBatchOperation();
            operaciones = operaciones.OrderBy((TableOperation x) => x.Entity.PartitionKey).ToList();
            TableOperation operation2 = operaciones[0];
            string partitionKey = operation2.Entity.PartitionKey;
            batch.Add(operation2);
            for (int i = 1; i < operaciones.Count; i++)
            {
                operation2 = operaciones[i];
                if (object.Equals(partitionKey, operation2.Entity.PartitionKey) && batch.Count < 100)
                {
                    batch.Add(operation2);
                    continue;
                }

                await tabla.ExecuteBatchAsync(batch);
                batch.Clear();
                batch.Add(operation2);
            }

            if (batch.Count > 0)
            {
                await tabla.ExecuteBatchAsync(batch);
                batch.Clear();
            }
        }
    }
}
