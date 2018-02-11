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
           Login();
           //DeadLock();
        }

        /// <summary>
        /// Disable login button and show/hide notification
        /// </summary>
        private void ToggleLogInButton()
        {
            if (LoginButton.IsEnabled)
            {
                //Disable login button and show notification
                LoginButton.IsEnabled = false;
                System.Diagnostics.Debug.WriteLine("LOGIN button disabled");
                BusyIndicator.Visibility = Visibility.Visible;
            }
            else
            {
                //Enable LOGIN button
                Thread.Sleep(800);
                BusyIndicator.Visibility = Visibility.Hidden;
                LoginButton.IsEnabled = true;
                //LoginButton.Content = "LOGIN";
            }
        }

        /// <summary>
        /// Deadlock sample
        /// </summary>
        private void DeadLock()
        {
            System.Diagnostics.Debug.WriteLine("Dead-Lock -> start");

            var task = Task.Delay(1).ContinueWith((t) => {
                Dispatcher.Invoke(() => {});
            });

            //Cause to a deadlock since the UI is blocked by WAIT.
            //Therefore it is imposible to invoke UI -> DEADLOCK !!!
            task.Wait();
        }


        private async void Login()
        {
            try
            {
                //Disable login button and show notification
                ToggleLogInButton();

                //Do login 
                var result = await AsyncLogin();
                System.Diagnostics.Debug.WriteLine(result);
                LoginButton.Content = result;
            }
            catch (Exception ex)
            {
                Thread.Sleep(500);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                LoginButton.Content = ex.Message;
            }
            finally
            {
                ToggleLogInButton();
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
                var result = Task.Run(() =>
                {
                    //SimulateException();

                    //Proccess login task
                    LoginTask();

                    //Return value
                    return "Login Successful!";
                });

                return await result;
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
            Thread.Sleep(1000);
        }
    }
}
