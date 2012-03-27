using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace AzureStorageMetricsViewer.Entities
{
    public class MetricsCapacityEntity: TableServiceEntity
    {
        public MetricsCapacityEntity()
        {
            //PartitionKey = DateTime.UtcNow.ToString("MMddyyyy");

            //// Row key allows sorting, so we make sure the rows come back in time order.
            //RowKey = string.Format("{0:10}_{1}", DateTime.MaxValue.Ticks - DateTime.Now.Ticks, Guid.NewGuid());
        }

        public long Capacity { set; get; }
        public long ContainerCount { set; get; }
        public long ObjectCount { set; get; }

        public override string ToString()
        {
            return string.Format("TimeStamp:{0}\tCapacity:{1}\tContainerCount:{2}\tObjectCount:{3}",
                this.Timestamp, Capacity, ContainerCount, ObjectCount);
        }
    }
}
