using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.DataVisualization.Charting;
using AzureStorageMetricsViewer.Entities;
using System.Collections.ObjectModel;

namespace AzureStorageMetricsViewer
{
    /// <summary>
    /// Interaction logic for TimepointChart.xaml
    /// </summary>
    public partial class TimepointChart : UserControl
    {
        ObservableCollection<MetricsTransactionsEntity> _olist = null;
        public TimepointChart()
        {
            InitializeComponent();
            _olist = new ObservableCollection<MetricsTransactionsEntity>();
        }

        public IEnumerable<MetricsTransactionsEntity> EntityList
        {
            get { return (IEnumerable<MetricsTransactionsEntity>)GetValue(EntityListProperty); }
            set { SetValue(EntityListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EntityList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EntityListProperty =
            DependencyProperty.Register("EntityList", typeof(IEnumerable<MetricsTransactionsEntity>), typeof(TimepointChart)
            , new PropertyMetadata(
                new PropertyChangedCallback((dp, e) =>
                {
                    var ctl = dp as TimepointChart;
                    ctl._olist.Clear();
                    foreach (var mte in e.NewValue as IEnumerable<MetricsTransactionsEntity>)
                        ctl._olist.Add(mte);
                })));
    }
}
