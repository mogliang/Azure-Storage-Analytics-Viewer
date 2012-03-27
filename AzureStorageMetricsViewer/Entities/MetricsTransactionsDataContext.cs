using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace AzureStorageMetricsViewer.Entities
{
    public class MetricsTransactionsDataContext:TableServiceContext
    {
        public MetricsTransactionsDataContext(string baseAddress, Microsoft.WindowsAzure.StorageCredentials credentials)
            : base(baseAddress, credentials)
        { }

        public IQueryable<MetricsTransactionsEntity> MetricsTransactionsBlob
        {
            get
            {
                return this.CreateQuery<MetricsTransactionsEntity>("$MetricsTransactionsBlob");
            }
        }

        public IQueryable<MetricsTransactionsEntity> MetricsTransactionsTable
        {
            get
            {
                return this.CreateQuery<MetricsTransactionsEntity>("$MetricsTransactionsTable");
            }
        }

        public IQueryable<MetricsTransactionsEntity> MetricsTransactionsQueue
        {
            get
            {
                return this.CreateQuery<MetricsTransactionsEntity>("$MetricsTransactionsQueue");
            }
        }
    }
}
