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
using System.Windows.Shapes;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace AzureStorageMetricsViewer
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(LoginWindow_Loaded);
        }

        const string accountkey = "account_key";
        const string accountname = "account_name";
        IsolatedStorageSettings _settings;
        void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _settings = new IsolatedStorageSettings();

            if (_settings.ContainsKey(accountname) && _settings.ContainsKey(accountkey))
            {
                accountname_tb.Text = _settings[accountname];
                accountkey_tb.Text = _settings[accountkey];
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                CloudStorageAccount account = new CloudStorageAccount(
                            new StorageCredentialsAccountAndKey(
                                accountname_tb.Text,
                                accountkey_tb.Text), true);

                CloudBlobClient client = account.CreateCloudBlobClient();

                var list = client.ListBlobsWithPrefix("_prefix_").ToList();

            }
            catch (Exception ex)
            {
                msg_tb.Text = "Unable to connect azure storage";
            }
        }

        private void link_Click_2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process
                .Start("http://www.cerebrata.com/Blog/post/Cerebrata-Windows-Azure-Storage-Analytics-Configuration-Utility-A-Free-Utility-to-Configure-Windows-Azure-Storage-Analytics.aspx");
        }
    }
}
