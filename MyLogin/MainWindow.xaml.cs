using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MyLogin
{
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();
        }

        /// <summary>
        /// Async event handler for login button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            AsyncLogin();
        }

        /// <summary>
        /// Async login method
        /// </summary>
        private async void AsyncLogin()
        {
            //Log
            System.Diagnostics.Debug.WriteLine("LOGIN button (async method) clicked");

            //Task
            var result = await Task.Run(() =>
            {
                //Proccess login task
                LoginTask();
                return "Login Successful!";
            });

            //Change LOGIN button lable to default
            LoginButton.Content = "LOGIN";
        }

        /// <summary>
        /// Login task
        /// </summary>
        /// <param name=""></param>
        public void LoginTask()
        {
            //Disable login button and show notification
            Dispatcher.Invoke(() => {
                LoginButton.Content = "Please wait...";
                LoginButton.IsEnabled = false;
                System.Diagnostics.Debug.WriteLine("LOGIN button disabled");
            });

            //Process user login task
            Thread.Sleep(6000);

            //Log
            System.Diagnostics.Debug.WriteLine("LOGIN button task finished");

            Dispatcher.Invoke(
                () => {
                    LoginButton.Content = "Login Successful!";
                });

            Thread.Sleep(800);

            //Enable LOGIN button
            Dispatcher.Invoke(
                () => {
                    LoginButton.IsEnabled = true;
                });         
        }
    }
}
