using LibraryAppClient.DataProviders;
using LibraryAppClient.Modells;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryAppClient
{
    /// <summary>
    /// Interaction logic for BookEditor.xaml
    /// </summary>
    public partial class BookEditor : Window
    {
        private static readonly ILog log
        = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Book _book;
        public BookEditor(Book B)
        {
            InitializeComponent();

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            if (B != null)
            {
                _book = B;
                BookTitleTextBox.Text = _book.BookTitle;
                AddButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                _book = new Book();
                EditButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
                BorrowedLabel.Visibility = Visibility.Collapsed;
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                _book.BookTitle = BookTitleTextBox.Text;
                BookDataProvider.Create(_book);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (_book.IsBorrowed)
            {
                BookError.Content = "A könyv nem szerkeszthető mert ki van kölcsönözve!";
                BookError.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    BookError.Visibility = Visibility.Collapsed;
                    _book.BookTitle = BookTitleTextBox.Text;
                    BookDataProvider.Update(_book);
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (_book.IsBorrowed)
            {
                BookError.Content = "A könyv nem törölhető mert ki van kölcsönözve!";
                BookError.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    BookError.Visibility = Visibility.Collapsed;
                    BookDataProvider.DeleteById(_book.Id);
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }
            }
        }
    }
}
