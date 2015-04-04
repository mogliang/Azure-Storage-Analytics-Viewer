#Azure Storage Analytics Viewer

Azure storage analytics is a very useful feature. It can help to isolate and debug Storage related issue. However, the metrics and log is not quite readable for users.

Azure Storage analytics viewer is a powerful tool which can make the analytics data eaiser to fetch and view. Features are listed below:
- Retrive the log data based on given time range and save to Excel format for further analysis.
- Plotting the storage metrics.
- Some simple filter option.

Binary download link:
[release page](https://github.com/mogliang/Azure-Storage-Analytics-Viewer/releases)

## Enable Azure Storage analytics

By default azure storage analytics is disabled. User need enable azure storage analytics from Azure portal.

![Enable Azure storage analytics](/docimages/portal.png)

## Plotting Azure Storage Metrics
1. Input storage account name and key.
2. Select time range. By default, it select one day before now.
3. Select the storage type which you want to view.
4. Click button to metrics data.
5. Click “Download Metrics” to save metrics data to local csv file.

![plotting metrics](/docimages/metrics.png)

## Download Azure Storage Logs
6. Click the datapoint on chart, all metrics properties will show on right side panel.
7. Click “Download Log” to download transaction log and save to local csv file.

![download logs](/docimages/logging.png)
