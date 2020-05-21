using LibraryAppClient.DataProviders;
using LibraryAppClient.Modells;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;


namespace LibraryAppClient
{
    /// <summary>
    /// Interaction logic for LibraryManager.xaml
    /// </summary>
    public partial class LibraryManager : Window
    {
        private static readonly ILog log
        = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public LibraryManager()
        {
            InitializeComponent();

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            LoadReaderData();
            LoadBookData();
            LoadBorrowedBooksList();
        }

        private void LoadReaderData()
        {
            try
            {
                ReaderList.ItemsSource = ReaderDataProvider.GetAllAsList();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        private void LoadBookData()
        {
            try
            {
                BookList.ItemsSource = BookDataProvider.GetAllAsList();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

        }
        private void LoadBorrowedBooksList()
        {
            BorrowedBooksList.ItemsSource = BorrowedBookDataProvider.GetAllAsList();
        }

        private void AddNewBorrow(object sender, RoutedEventArgs e)
        {
            var window = new BorrowEditor(null);
            if (window.ShowDialog() ?? false)
            {
                LoadBorrowedBooksList();
                LoadBookData();
            }
        }

        private void BorrowedBooksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBorrow = BorrowedBooksList.SelectedItem as BorrowedBook;

            if (selectedBorrow != null)
            {
                var window = new BorrowEditor(selectedBorrow);
                if (window.ShowDialog() ?? false)
                {
                    LoadBorrowedBooksList();
                }
                LoadBookData();
                BorrowedBooksList.UnselectAll();
            }
        }

        private void ReaderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedReader = ReaderList.SelectedItem as Reader;

            if (selectedReader != null)
            {
                var window = new ReaderEditor(selectedReader);
                if (window.ShowDialog() ?? false)
                {
                    LoadBorrowedBooksList();
                    LoadReaderData();
                }
                ReaderList.UnselectAll();
            }
        }

        private void BookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBook = BookList.SelectedItem as Book;

            if (selectedBook != null)
            {
                var window = new BookEditor(selectedBook);
                if (window.ShowDialog() ?? false)
                {
                    LoadBorrowedBooksList();
                    LoadBookData();
                }
                BookList.UnselectAll();
            }
        }

        private void New_Book_Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new BookEditor(null);
            if (window.ShowDialog() ?? false)
            {
                LoadBorrowedBooksList();
                LoadBookData();
            }
        }

        private void New_Reader_Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new ReaderEditor(null);
            if (window.ShowDialog() ?? false)
            {
                LoadBorrowedBooksList();
                LoadReaderData();
            }
        }

        private void SearchByReaderName(object sender, RoutedEventArgs e)
        {
            try
            {
                List<BorrowedBook> FullList = BorrowedBookDataProvider.GetAllAsList() as List<BorrowedBook>;
                BorrowedBooksList.ItemsSource = FullList.FindAll(x => x.ReaderName == ReaderNameTextBox.Text);
            }catch(Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        private void SearchByBookName(object sender, RoutedEventArgs e)
        {
            try
            {
                List<BorrowedBook> FullList = BorrowedBookDataProvider.GetAllAsList() as List<BorrowedBook>;
                BorrowedBooksList.ItemsSource = FullList.FindAll(x => x.BookName == BookNameTextBox.Text);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        private void SearchByReaderID(object sender, RoutedEventArgs e)
        {
            try
            {
                List<BorrowedBook> FullList = BorrowedBookDataProvider.GetAllAsList() as List<BorrowedBook>;
                BorrowedBooksList.ItemsSource = FullList.FindAll(x => x.ReaderNumber == long.Parse(ReaderIDTextBox.Text));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        private void SearchByBookID(object sender, RoutedEventArgs e)
        {
            try
            {
                List<BorrowedBook> FullList = BorrowedBookDataProvider.GetAllAsList() as List<BorrowedBook>;
                BorrowedBooksList.ItemsSource = FullList.FindAll(x => x.BookNumber == long.Parse(BookIDTextBox.Text));

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        private void LoadAll(object sender, RoutedEventArgs e)
        {
            LoadBorrowedBooksList();
        }
    }
}

