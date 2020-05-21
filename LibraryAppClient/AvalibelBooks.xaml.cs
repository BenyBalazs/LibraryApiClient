using LibraryAppClient.DataProviders;
using LibraryAppClient.Modells;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AvalibelBooks.xaml
    /// </summary>
    public partial class AvalibelBooks : Window
    {
        public AvalibelBooks()
        {
            InitializeComponent();
            List<Book> ListOfAllBook = BookDataProvider.GetAllAsList() as List<Book>;
            BookList.ItemsSource = ListOfAllBook.FindAll(x => x.IsBorrowed == false);
        }
    }
}
