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
using AzureStorageMetricsViewer.Entities;
using System.Windows.Controls.DataVisualization.Charting;
using System.Collections.ObjectModel;

namespace AzureStorageMetricsViewer
{
    /// <summary>
    /// Interaction logic for TimelineChart.xaml
    /// </summary>
    public partial class TimelineChart : UserControl
    {
        ObservableCollection<MetricsTransactionsEntity> _olist = null;
        public TimelineChart()
        {
            InitializeComponent();
            InitializeList();

            _olist = new ObservableCollection<MetricsTransactionsEntity>();
            //this.Loaded += new RoutedEventHandler(TimelineChart_Loaded);
        }

        public void InitializeList()
        {
            List<PropertyCheck> pclist = new List<PropertyCheck>();
            Type mte = typeof(MetricsTransactionsEntity);
            contextmenu1.Items.Clear();
            foreach (var prop in mte.GetProperties())
            {
                var pc = new PropertyCheck
                {
                    Name = prop.Name,
                };
                pclist.Add(pc);

                
                var item = new MenuItem
                    {
                        Header = prop.Name,
                        IsCheckable = true,
                        DataContext=pc
                    };
                item.Checked += new RoutedEventHandler(item_Checked);
                item.Unchecked += new RoutedEventHandler(item_Unchecked);
                contextmenu1.Items.Add(item);
            }
        }

        public void RemoveSeries(string propname)
        {
            var mi = contextmenu1.Items.Cast<MenuItem>().First(i => propname.CompareTo(i.Header) == 0);
            if (mi.IsChecked)
                mi.IsChecked = false;
        }
        void item_Unchecked(object sender, RoutedEventArgs e)
        {
            var pc = ((FrameworkElement)sender).DataContext as PropertyCheck;
            var dels = chart0.Series.FirstOrDefault(s => ((FrameworkElement)s).Name == pc.Name);
            chart0.Series.Remove(dels);
        }

        public void AddSeries(string propname)
        {
            var mi = contextmenu1.Items.Cast<MenuItem>().First(i => propname.CompareTo(i.Header) == 0);
            if (!mi.IsChecked)
                mi.IsChecked = true;
        }
        void item_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var pc = ((FrameworkElement)sender).DataContext as PropertyCheck;
                LineSeries ls = new LineSeries();
                ls.Title = pc.Name;
                ls.IndependentValuePath = "Time";
                ls.DependentValuePath = pc.Name;
                ls.Name = pc.Name;
                ls.IsSelectionEnabled = true;

                ls.IndependentAxis = dta1;
                //Style labelstyle = new Style(typeof(DateTimeAxisLabel));
                //labelstyle.Setters.Add(new Setter(DateTimeAxisLabel.HoursIntervalStringFormatProperty, "{0}"));
                //((DateTimeAxis)ls.IndependentAxis).AxisLabelStyle = labelstyle;

                // Customize datapoint
                //var dpstyle = App.Current.Resources["LineDataPointStyle1"] as Style;
                //var newstyle = new Style();
                //newstyle.BasedOn=dpstyle;
                //newstyle.TargetType = typeof(LineDataPoint);
                //newstyle.Setters.Add(
                //    new Setter(
                //        Control.BackgroundProperty,
                //        GetRandomBrush()));
                //ls.DataPointStyle = newstyle;

                ls.OverridesDefaultStyle = false;
                ls.ItemsSource = _olist;
                ls.IsSelectionEnabled = true;
                ls.SelectionChanged += new SelectionChangedEventHandler(ls_SelectionChanged);

                chart0.Series.Add(ls);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;
        void ls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }

        public IEnumerable<MetricsTransactionsEntity> EntityList
        {
            get { return (IEnumerable<MetricsTransactionsEntity>)GetValue(EntityListProperty); }
            set { SetValue(EntityListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EntityList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EntityListProperty =
            DependencyProperty.Register("EntityList", typeof(IEnumerable<MetricsTransactionsEntity>), typeof(TimelineChart)
            , new PropertyMetadata(
                new PropertyChangedCallback((dp, e) =>
                    {
                        var ctl = dp as TimelineChart;

                        ctl._olist.Clear();
                        foreach (var mte in e.NewValue as IEnumerable<MetricsTransactionsEntity>)
                            ctl._olist.Add(mte);
                    })));

        Color[] clist = {
                        Colors.AliceBlue,
                        Colors.Aqua,
                        Colors.Beige,
                        Colors.Black,
                        Colors.BlanchedAlmond,
                        Colors.Blue};
        Random _rand = new Random();
        Brush GetRandomBrush()
        {
            var idx = _rand.Next() % clist.Count();
            return new SolidColorBrush(
                clist[idx]);
        }


        void TimelineChart_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeList();
        }
    }
}
