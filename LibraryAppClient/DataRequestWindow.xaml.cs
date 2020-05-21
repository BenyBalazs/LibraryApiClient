using LibraryAppClient.DataProviders;
using LibraryAppClient.Modells;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for DataRequestWindow.xaml
    /// </summary>
    public partial class DataRequestWindow : Window
    {
        private static List<BorrowedBook> AllBooks;
        public DataRequestWindow(long borrowerId)
        {
            InitializeComponent();
            AllBooks = BorrowedBookDataProvider.GetAllAsList() as List<BorrowedBook>;
            SortById(borrowerId);
            AddItems();
            HaveAnyElements(AllBooks);
        }
        private void HaveAnyElements(IEnumerable<BorrowedBook> borrowedBooks)
        {
            if (!borrowedBooks.Any())
            {
                AllBorrowedBooks.Visibility = Visibility.Hidden;
                NotFound.Visibility = Visibility.Visible;
            }
        }
        private void SortById(long id)
        {
            var temp = AllBooks.FindAll(x => x.ReaderNumber == id).ToList();
            AllBooks = temp;
        }
        private void AddItems()
        {
            foreach (var item in AllBooks)
            {
                AllBorrowedBooks.Items.Add(item);
            }           
        }
    }
}
