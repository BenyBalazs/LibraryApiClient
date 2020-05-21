using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryAppClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog log
       = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public MainWindow()
        {
            InitializeComponent();

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var convert = IdBox.Text;
                DataRequestWindow DataWindow = new DataRequestWindow(Int32.Parse(convert));
                DataWindow.Show();
                log.Info("Opening DataRequestWindwo");
            }catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Authenticator.LogIn(Username.Text, Password.Password))
            {
                // Open New window
                AuthenticationFail.Visibility = Visibility.Hidden;
                log.Info("Authentication Successful");
                OpenLibrarayManagerWindwo();
                this.Close();
            }
            else
            { 
                AuthenticationFail.Visibility = Visibility.Visible;
                log.Info("Authentication Failed");
            }
        }

        private void OpenLibrarayManagerWindwo()
        {
            try
            {
                LibraryManager librarayManagerWindow = new LibraryManager();
                librarayManagerWindow.Show();
            }catch (Exception ex)
            {
                log.Error("Failed to create window", ex);
            }
        }

        private void Open_Avalible_Book_List(object sender, RoutedEventArgs e)
        {
            var window = new AvalibelBooks();
            window.Show();
        }
    }
}
