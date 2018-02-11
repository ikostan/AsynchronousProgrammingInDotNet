using Android.App;
using Android.Widget;
using Android.OS;
using System.Net;
using System;
using System.Threading;

namespace ReliableAndroidApplication
{
    [Activity(Label = "ReliableAndroidApplication", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private int count = 1;
        private Button rssButton;
        private ProgressDialog progressDialog;
        private TextView rssTextView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var counterButton = FindViewById<Button>(Resource.Id.CounterButton);
            rssButton = FindViewById<Button>(Resource.Id.RssButton);
            rssTextView = FindViewById<TextView>(Resource.Id.Rss);
            progressDialog = new ProgressDialog(this);
            progressDialog.Hide();

            counterButton.Click += delegate {
                var counterTextView = FindViewById<TextView>(Resource.Id.CounterText);
                counterTextView.Text = $"Counter {count++}";
            };

            rssButton.Click += delegate {
                var client = new WebClient();

                //Old code
                /*
                var data = client.DownloadString("http://www.filipekberg.se/rss/");
                Thread.Sleep(10000);
                var rssTextView = FindViewById<TextView>(Resource.Id.Rss);
                rssTextView.Text = data;
                */
                progressDialog.Show();
                rssButton.Enabled = false;
                Uri uri = new Uri("http://www.filipekberg.se/rss/");
                client.DownloadStringAsync(uri);
                client.DownloadStringCompleted += StringDownloadCompleted;
            };
        }

        /// <summary>
        /// DownloadStringCompleted event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StringDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {           
            rssTextView.Text = e.Result.ToString();
            progressDialog.Hide();
            rssButton.Enabled = true;
        }
    }
}

