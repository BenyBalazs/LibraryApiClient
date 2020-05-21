using LibraryAppClient.Modells;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace LibraryAppClient.DataProviders
{
    class BorrowedBookDataProvider 
    {
        private static string url = "http://localhost:5000/api/borrowedbooks";
        public static IList<BorrowedBook> GetAllAsList()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var borrowedBooks = JsonConvert.DeserializeObject<IList<BorrowedBook>>(rawData);
                    return borrowedBooks;
                }
                else
                    throw new InvalidOperationException(response.StatusCode.ToString());
            }
        }
        public static BorrowedBook GetById(long id)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url + "/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var borrowedBooks = JsonConvert.DeserializeObject<BorrowedBook>(rawData);
                    return borrowedBooks;
                }
                else
                    throw new InvalidOperationException(response.StatusCode.ToString());
            }
        }
        public static void Create(BorrowedBook borrowedBook)
        {
            using (var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(borrowedBook);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content).Result;

                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException(response.StatusCode.ToString());

            }
        }
        public static void Update(BorrowedBook borrowedBook)
        {
            using (var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(borrowedBook);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PutAsync(url + "/" + borrowedBook.Id, content).Result;

                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException(response.StatusCode.ToString());

            }
        } 
        public static void DeleteById(long Id)
        {
            using (var client = new HttpClient())
            {

                var response = client.DeleteAsync(url + "/" + Id).Result;

                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException(response.StatusCode.ToString());

            }
        }
    }
}
