using System;
using System.Net;
using System.Threading;
using System.Windows;

namespace ReliableWPFApplication
{
    public partial class MainWindow : Window
    {
        private int count = 1;
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();
        }

        private void RssButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new WebClient();

            //var data = client.DownloadString("http://www.filipekberg.se/rss/"); // Old code
            //Thread.Sleep(10000); // Old code
            //RssText.Text = data; // Old code

            //New Code
            RssButton.IsEnabled = false;
            BusyIndicator.Visibility = Visibility.Visible;
            Uri uri = new Uri("http://www.filipekberg.se/rss/");
            client.DownloadStringAsync(uri);
            client.DownloadStringCompleted += DataDownloadCompleted;          
        }

        /// <summary>
        /// DownloadStringCompleted  event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            BusyIndicator.Visibility = Visibility.Hidden;
            RssButton.IsEnabled = false;
            RssText.Text = e.Result.ToString();
        }

        private void CounterButton_Click(object sender, RoutedEventArgs e)
        {
            CounterText.Text = $"Counter: {count++}";
        }
    }
}
