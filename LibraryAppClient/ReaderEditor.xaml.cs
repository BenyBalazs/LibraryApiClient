using LibraryAppClient.DataProviders;
using LibraryAppClient.Modells;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// Interaction logic for ReaderEditor.xaml
    /// </summary>
    public partial class ReaderEditor : Window
    {
        private static readonly ILog log
        = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Reader _reader;

        private readonly bool _hasBorrow;

        private static bool NamesAreValid = false;
        public ReaderEditor(Reader R)
        {
            InitializeComponent();
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            if (R != null)
            {
                _reader = R;
                FirstNameBox.Text = _reader.FirstName;
                LastNameBox.Text = _reader.LastName;
                AddButton.Visibility = Visibility.Collapsed;

                List<BorrowedBook> CurrentBorrow = BorrowedBookDataProvider.GetAllAsList() as List<BorrowedBook>;

                var Filtered = CurrentBorrow.FirstOrDefault(x => x.ReaderNumber == _reader.Id);

                log.Info($"{Filtered}");

                if (Filtered != null)
                    _hasBorrow = true;
                else
                    _hasBorrow = false;
            }
            else
            {
                _reader = new Reader();
                DeleteButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {

            try
            {
                SetFirstName(FirstNameBox.Text);
                SetLastName(LastNameBox.Text);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            if (NamesAreValid)
            {
                ReaderDataProvider.Create(_reader);
                DialogResult = true;
                Close();
            }
        }


        private void Edit(object sender, RoutedEventArgs e)
        {

            if (_hasBorrow)
            {
                CommitError.Content = "Az olvasó nem szerkeszthető mert van kikölcsönzött könyve";
                CommitError.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    CommitError.Visibility = Visibility.Visible;
                    SetFirstName(FirstNameBox.Text);
                    SetLastName(LastNameBox.Text);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }

                if (NamesAreValid)
                {
                    ReaderDataProvider.Update(_reader);
                    DialogResult = true;
                    Close();
                }
            }
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            if (_hasBorrow)
            {
                CommitError.Content = "Az olvasó nem törölhető mert van kikölcsönzött könyve";
                CommitError.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    ReaderDataProvider.DeleteById(_reader.Id);
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }
            }
        }
        private void SetLastName(string text)
        {
            try
            {
                _reader.LastName = text;
                NamesAreValid &= true;
            }catch(Exception e)
            {
                NamesAreValid = false;
                LastNameError.Visibility = Visibility.Visible;
                log.Error(e.Message);
            }
            if (text.Length == 0)
            {
                NamesAreValid = false;
                LastNameError.Visibility = Visibility.Visible;
            }
        }
        
        private void SetFirstName(string text)
        {
            try
            {
                _reader.FirstName = text;
                NamesAreValid = true;
            }
            catch (Exception e)
            {
                NamesAreValid = false;
                FirstNameError.Visibility = Visibility.Visible;
                log.Error(e.Message);
            }
            if (text.Length == 0)
            {
                NamesAreValid = false;
                FirstNameError.Visibility = Visibility.Visible;
            }
        }
    }
}

