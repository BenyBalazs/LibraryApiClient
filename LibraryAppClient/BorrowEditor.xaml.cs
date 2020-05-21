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
    /// Interaction logic for BorrowEditor.xaml
    /// </summary>
    public partial class BorrowEditor : Window
    {
        private static readonly ILog log
        = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly BorrowedBook _borrowedBook;
        public BorrowEditor(BorrowedBook BB)
        {
            InitializeComponent();
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            Borrower.ItemsSource = ReaderDataProvider.GetAllAsList();
            List<Book> BookList = BookDataProvider.GetAllAsList() as List<Book>;
            BookToLend.ItemsSource = BookList.FindAll(x => x.IsBorrowed == false);

            if (!(BB == null))
            {
                _borrowedBook = BB;
                CurrantBorrower.Content = _borrowedBook.ReaderName;
                CurrantBorrower.Visibility = Visibility.Visible;
                CurrantBook.Content = _borrowedBook.BookName;
                CurrantBook.Visibility = Visibility.Visible;
                StartOfBorrow.SelectedDate = _borrowedBook.DateOfBorrow;
                DateOfDeadline.SelectedDate = _borrowedBook.Deadline;

                AddButton.Visibility = Visibility.Hidden;
            }
            else {
                _borrowedBook = new BorrowedBook();
                DeleteButton.Visibility = Visibility.Hidden;
                SaveButton.Visibility = Visibility.Hidden;
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            var CurrantBook = BookDataProvider.GetById(_borrowedBook.BookNumber);
            var BookInstence = BookToLend.SelectedItem as Book;
            var ReaderInstence = Borrower.SelectedItem as Reader;

            if (ReaderInstence == null && BookInstence != null)
            {
                CurrantBook.IsBorrowed = false;
                BookDataProvider.Update(CurrantBook);
                _borrowedBook.BookNumber = BookInstence.Id;
                _borrowedBook.BookName = BookInstence.BookTitle;
                var NewBook = BookToLend.SelectedItem as Book;
                NewBook.IsBorrowed = true;
                BookDataProvider.Update(NewBook);
                BorrowedBookDataProvider.Update(_borrowedBook);
                BookSaved.Visibility = Visibility.Visible;
            }
            else if (BookInstence == null && ReaderInstence != null)
            {
                _borrowedBook.ReaderNumber = ReaderInstence.Id;
                _borrowedBook.ReaderName = $"{ReaderInstence.FirstName} {ReaderInstence.LastName}";
                BorrowedBookDataProvider.Update(_borrowedBook);
                BorrowerSave.Visibility = Visibility.Visible;

            }
            else if (ReaderInstence != null && BookInstence != null)
            {
                CurrantBook.IsBorrowed = false;
                BookDataProvider.Update(CurrantBook);
                _borrowedBook.BookNumber = BookInstence.Id;
                _borrowedBook.BookName = BookInstence.BookTitle;
                _borrowedBook.ReaderNumber = ReaderInstence.Id;
                _borrowedBook.ReaderName = $"{ReaderInstence.FirstName} {ReaderInstence.LastName}";
                var NewBook = BookToLend.SelectedItem as Book;
                NewBook.IsBorrowed = true;
                BookDataProvider.Update(NewBook);
                BorrowedBookDataProvider.Update(_borrowedBook);
                BorrowerSave.Visibility = Visibility.Visible;
                BookSaved.Visibility = Visibility.Visible;
            }
            if (DateValidator.ValidateDate(StartOfBorrow.SelectedDate.Value, DateOfDeadline.SelectedDate.Value))
            {
                _borrowedBook.DateOfBorrow = StartOfBorrow.SelectedDate.Value;
                _borrowedBook.Deadline = DateOfDeadline.SelectedDate.Value;
                BorrowedBookDataProvider.Update(_borrowedBook);
                DialogResult = true;
                Close();
            }
            else
            {
                DeadlineError.Content = "Nem jók a dátumok!";
                DeadlineError.Visibility = Visibility.Visible;
                BorrowDateError.Content = "Nem jók a dátumok!";
                BorrowDateError.Visibility = Visibility.Visible;
            }

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AllFieldHasContent())
                {
                    if(DateValidator.ValidateDate(StartOfBorrow.SelectedDate.Value, DateOfDeadline.SelectedDate.Value))
                    {
                        var BookInstence = BookToLend.SelectedItem as Book;
                        var ReaderInstence = Borrower.SelectedItem as Reader;
                        BookInstence.IsBorrowed = true;
                        BookDataProvider.Update(BookInstence);
                        _borrowedBook.BookNumber = BookInstence.Id;
                        _borrowedBook.BookName = BookInstence.BookTitle;
                        _borrowedBook.ReaderNumber = ReaderInstence.Id;
                        _borrowedBook.ReaderName = $"{ReaderInstence.FirstName} {ReaderInstence.LastName}";
                        _borrowedBook.DateOfBorrow = StartOfBorrow.SelectedDate.Value;
                        _borrowedBook.Deadline = DateOfDeadline.SelectedDate.Value;
                        BorrowedBookDataProvider.Create(_borrowedBook);
                        DialogResult = true;
                        Close();
                    }else
                    {
                        DeadlineError.Content = "Nem jók a dátumok!";
                        DeadlineError.Visibility = Visibility.Visible;
                        BorrowDateError.Content = "Nem jók a dátumok!";
                        BorrowDateError.Visibility = Visibility.Visible;
                    }

                }
            }catch (Exception ex)
            {
                log.Error(ex.Message);
            }

        }

        private bool AllFieldHasContent()
        {
            bool NotEmpty = true;

            if (BookToLend.SelectedItem == null)
            {
                BookError.Content = "Ki kell választani a könyvet!";
                BookError.Visibility = Visibility.Visible;
                NotEmpty = false;
            }
            if (Borrower.SelectedItem == null)
            {
                BorrowerError.Content = "Ki kell választani a könyvet!";
                BorrowerError.Visibility = Visibility.Visible;
                NotEmpty = false;
            }
            if (DateOfDeadline.SelectedDate == null)
            {
                DeadlineError.Content = "Ki kell választani egy dátumot!";
                DeadlineError.Visibility = Visibility.Visible;
                NotEmpty = false;
            }
            if (StartOfBorrow.SelectedDate == null)
            {
                BorrowDateError.Content = "Ki kell választani egy dátumot!";
                BorrowDateError.Visibility = Visibility.Visible;
                NotEmpty = false;
            }

            return NotEmpty;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var CurrantBook = BookDataProvider.GetById(_borrowedBook.BookNumber);
            CurrantBook.IsBorrowed = false;
            BookDataProvider.Update(CurrantBook);
            BorrowedBookDataProvider.DeleteById(_borrowedBook.Id);
            DialogResult = true;
            Close();
        }
    }
}
