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
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                //Disable login button and show notification
                LoginButton.IsEnabled = false;
                System.Diagnostics.Debug.WriteLine("LOGIN button disabled");
                BusyIndicator.Visibility = Visibility.Visible;

                //Do login 
                var result = await AsyncLogin();
                System.Diagnostics.Debug.WriteLine(result);
                LoginButton.Content = result;          
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                LoginButton.Content = ex.Message;
            }
            finally
            {
                //Enable LOGIN button
                Thread.Sleep(800);
                BusyIndicator.Visibility = Visibility.Hidden;
                //LoginButton.Content = "LOGIN";
                LoginButton.IsEnabled = true;
            }   
        }

        /// <summary>
        /// Async login method
        /// </summary>
        private async Task<string> AsyncLogin()
        {
            //Log
            System.Diagnostics.Debug.WriteLine("LOGIN button (async method) clicked");

            //Symulate exception on random basis
            SimulateException();

            try
            {
                //Task
                var result = await Task.Run(() =>
                {
                    //SimulateException();

                    //Proccess login task
                    LoginTask();

                    //Return value
                    return "Login Successful!";
                });

                return result;
            }
            catch (Exception)
            {
                return "Login Failed!";
            }
        }

        /// <summary>
        /// Simulate an error/exception
        /// </summary>
        private void SimulateException()
        {
            Random rnd = new Random();
            int i = rnd.Next(2) + 1;
            if (i % 2 != 0)
            {
                string error = "Login Failed!";
                System.Diagnostics.Debug.WriteLine(error);
                throw new UnauthorizedAccessException(error);
            }
        }

        /// <summary>
        /// Login task
        /// </summary>
        /// <param name=""></param>
        public void LoginTask()
        {
            //Process user login task
            Thread.Sleep(2000);
        }
    }
}
