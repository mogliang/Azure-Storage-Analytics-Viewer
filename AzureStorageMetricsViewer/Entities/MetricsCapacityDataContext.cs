using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace AzureStorageMetricsViewer.Entities
{
    public class MetricsCapacityDataContext:TableServiceContext
    {
        public MetricsCapacityDataContext(string baseAddress, Microsoft.WindowsAzure.StorageCredentials credentials)
            : base(baseAddress, credentials)
        { }

        public IQueryable<MetricsCapacityEntity> MetricsCapacityBlob
        {
            get
            {
                return this.CreateQuery<MetricsCapacityEntity>("$MetricsCapacityBlob");
            }
        }
    }
}
