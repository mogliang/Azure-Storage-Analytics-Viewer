using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace AzureStorageMetricsViewer.Entities
{
    public class MetricsTransactionsEntity: TableServiceEntity
    {
        public MetricsTransactionsEntity()
        {
            TimeSpan = TimeSpan.FromHours(1);
            //PartitionKey = DateTime.UtcNow.ToString("MMddyyyy");

            //// Row key allows sorting, so we make sure the rows come back in time order.
            //RowKey = string.Format("{0:10}_{1}", DateTime.MaxValue.Ticks - DateTime.Now.Ticks, Guid.NewGuid());
        }

        // parse YYYYMMddThhmm
        private DateTime ParseDateString(string datestr)
        {
            int year = int.Parse(datestr.Substring(0, 4));
            int month = int.Parse(datestr.Substring(4, 2));
            int day = int.Parse(datestr.Substring(6, 2));
            int hour = int.Parse(datestr.Substring(9, 2));
            int minute = int.Parse(datestr.Substring(11, 2));

            return new DateTime(year, month, day, hour, minute, 0);
        }

        public MetricsTransactionsEntity Merge(MetricsTransactionsEntity b)
        {
            var ret = new MetricsTransactionsEntity();

            ret.PartitionKey = this.PartitionKey;
            ret.RowKey = this.RowKey;
            ret.TimeSpan = this.TimeSpan + b.TimeSpan;
            
            ret.TotalIngress = this.TotalEgress + b.TotalIngress;
            ret.TotalEgress = this.TotalEgress + b.TotalEgress;
            ret.TotalRequests = this.TotalRequests + b.TotalRequests;
            ret.TotalBillableRequests = this.TotalBillableRequests + b.TotalBillableRequests;

            ret.PercentSuccess = (this.Success + b.Success) /
                ((this.Success * 100 / this.PercentSuccess) + (b.Success * 100 / b.PercentSuccess));

            //long ac =this.ThrottlingError+this.SASThrottlingError+this.AnonymousThrottlingError;
            //long bc =b.ThrottlingError+b.SASThrottlingError+b.AnonymousThrottlingError;
            //ret.PercentThrottlingError = (ac+bc) /
            //    ((ac * 100 / this.PercentThrottlingError) + (bc * 100 / b.PercentThrottlingError));

            //long ac2= this.AnonymousClientTimeoutError+this.AnonymousServerTimeoutError+
            //    this.SASClientTimeoutError+this.SASServerTimeoutError+
            //    this.ClientTimeoutError+this.ServerTimeoutError;
            //long bc2 = b.AnonymousClientTimeoutError + b.AnonymousServerTimeoutError +
            //    b.SASClientTimeoutError + b.SASServerTimeoutError +
            //    b.ClientTimeoutError + b.ServerTimeoutError;
            //ret.PercentTimeoutError = (ac2 + bc2) /
            //    ((ac2 * 100 / this.PercentTimeoutError) + (bc2 * 100 / b.PercentTimeoutError));

            //ret.PercentClientOtherError = (this.ClientOtherError + b.ClientOtherError) /
            //    ((this.ClientOtherError * 100 / this.PercentClientOtherError) + (b.ClientOtherError * 100 / b.PercentClientOtherError));

            //ret.PercentServerOtherError = (this.ServerOtherError + b.ServerOtherError) /
            //    ((this.ServerOtherError * 100 / this.PercentServerOtherError) + (b.ServerOtherError * 100 / b.PercentServerOtherError));

            //long ac3 = this.AuthorizationError + this.AnonymousAuthorizationError + this.SASAuthorizationError;
            //long bc3 = b.AuthorizationError + b.AnonymousAuthorizationError + b.SASAuthorizationError;
            //ret.PercentAuthorizationError = (ac3 + bc3) /
            //    ((ac3 * 100 / this.PercentAuthorizationError) + (bc3 * 100 / b.PercentAuthorizationError));

            //long ac4 = this.NetworkError + this.AnonymousNetworkError + this.SASNetworkError;
            //long bc4 = b.NetworkError + b.AnonymousNetworkError + b.SASNetworkError;
            //ret.PercentAuthorizationError = (ac4 + bc4) /
            //    ((ac4 * 100 / this.PercentNetworkError) + (bc4 * 100 / b.PercentNetworkError));

            // possible issue
            ret.PercentThrottlingError = (this.PercentThrottlingError + b.PercentThrottlingError) / 2;
            ret.PercentTimeoutError = (this.PercentTimeoutError + b.PercentTimeoutError) / 2;
            ret.PercentNetworkError = (this.PercentNetworkError + b.PercentNetworkError) / 2;
            ret.PercentAuthorizationError = (this.PercentAuthorizationError + b.PercentAuthorizationError) / 2;
            ret.PercentClientOtherError = (this.PercentClientOtherError + b.PercentClientOtherError) / 2;
            ret.PercentServerOtherError = (this.PercentServerOtherError + b.PercentServerOtherError) / 2;
            ret.PercentSuccess = (this.PercentSuccess + b.PercentSuccess) / 2;

            ret.AverageE2ELatency = (this.AverageE2ELatency + b.AverageE2ELatency) / 2;
            ret.AverageServerLatency = (this.AverageServerLatency + b.AverageServerLatency) / 2;
            ret.Availability = (this.Availability + b.Availability) / 2;

            ret.Success = this.Success + b.Success;
            ret.AnonymousSuccess = this.AnonymousSuccess + b.AnonymousSuccess;
            ret.SASSuccess = this.SASSuccess + b.SASSuccess;

            ret.ThrottlingError = this.ThrottlingError + b.ThrottlingError;
            ret.AnonymousThrottlingError = this.AnonymousThrottlingError + b.AnonymousThrottlingError;
            ret.SASThrottlingError = this.SASThrottlingError + b.SASThrottlingError;

            ret.ClientTimeoutError = this.ClientTimeoutError + b.ClientTimeoutError;
            ret.AnonymousClientTimeoutError = this.AnonymousClientTimeoutError + b.AnonymousClientTimeoutError;
            ret.SASClientTimeoutError = this.SASClientTimeoutError + b.SASClientTimeoutError;

            ret.ServerTimeoutError = this.ServerTimeoutError + b.ServerTimeoutError;
            ret.AnonymousServerTimeoutError = this.AnonymousServerTimeoutError + b.AnonymousServerTimeoutError;
            ret.SASServerTimeoutError = this.SASServerTimeoutError + b.SASServerTimeoutError;

            ret.ClientOtherError = this.ClientOtherError + b.ClientOtherError;
            ret.AnonymousClientOtherError = this.AnonymousClientOtherError + b.AnonymousClientOtherError;
            ret.SASClientOtherError = this.SASClientOtherError + b.SASClientOtherError;

            ret.ServerOtherError = this.ServerOtherError + b.ServerOtherError;
            ret.AnonymousServerOtherError = this.AnonymousServerOtherError + b.AnonymousServerOtherError;
            ret.SASServerOtherError = this.SASServerOtherError + b.SASServerOtherError;

            ret.AuthorizationError = this.AuthorizationError + b.AuthorizationError;
            ret.AnonymousAuthorizationError = this.AnonymousAuthorizationError + b.AnonymousAuthorizationError;
            ret.SASAuthorizationError = this.SASAuthorizationError + b.SASAuthorizationError;

            ret.NetworkError = this.NetworkError + b.NetworkError;
            ret.AnonymousNetworkError = this.AnonymousNetworkError + b.AnonymousNetworkError;
            ret.SASNetworkError = this.SASNetworkError + b.SASNetworkError;

            return ret;
        }

        DateTime? _time;
        public DateTime Time
        {
            get
            {
                if (!_time.HasValue)
                    _time = ParseDateString(PartitionKey);
                return _time.Value;
            }
        }
        public TimeSpan TimeSpan { set; get; }

        public long TotalIngress { set; get; }
        public long TotalEgress { set; get; }
        public long TotalRequests { set; get; }
        public long TotalBillableRequests { set; get; }

        public double Availability { set; get; }//
        public double AverageE2ELatency { set; get; }
        public double AverageServerLatency { set; get; }

        public double PercentSuccess { set; get; }
        public double PercentThrottlingError { set; get; }
        public double PercentTimeoutError { set; get; }
        public double PercentServerOtherError { set; get; }
        public double PercentClientOtherError { set; get; }
        public double PercentAuthorizationError { set; get; }
        public double PercentNetworkError { set; get; }

        public long Success { set; get; }
        public long AnonymousSuccess { set; get; }
        public long SASSuccess { set; get; }

        public long ThrottlingError { set; get; }
        public long AnonymousThrottlingError { set; get; }
        public long SASThrottlingError { set; get; }
        public long ClientTimeoutError { set; get; }
        public long AnonymousClientTimeoutError { set; get; }
        public long SASClientTimeoutError { set; get; }
        public long ServerTimeoutError { set; get; }
        public long AnonymousServerTimeoutError { set; get; }
        public long SASServerTimeoutError { set; get; }
        public long ClientOtherError { set; get; }
        public long SASClientOtherError { set; get; }
        public long AnonymousClientOtherError { set; get; }
        public long ServerOtherError { set; get; }
        public long AnonymousServerOtherError { set; get; }
        public long SASServerOtherError { set; get; }
        public long AuthorizationError { set; get; }
        public long AnonymousAuthorizationError { set; get; }
        public long SASAuthorizationError { set; get; }
        public long NetworkError { set; get; }
        public long AnonymousNetworkError { set; get; }
        public long SASNetworkError { set; get; }
    }
}
