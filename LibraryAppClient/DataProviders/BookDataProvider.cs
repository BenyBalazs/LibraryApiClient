using LibraryAppClient.Modells;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace LibraryAppClient.DataProviders
{
    class BookDataProvider
    {
        private static string url = "http://localhost:5000/api/books";
        public static IList<Book> GetAllAsList()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var books = JsonConvert.DeserializeObject<IList<Book>>(rawData);
                    return books;
                }
                else
                    throw new InvalidOperationException(response.StatusCode.ToString());
            }
        }
        public static Book GetById(long id)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url + "/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var book = JsonConvert.DeserializeObject<Book>(rawData);
                    return book;
                }
                else
                    throw new InvalidOperationException(response.StatusCode.ToString());
            }
        }
        public static void Create(Book book)
        {
            using(var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(book);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content).Result;

                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException(response.StatusCode.ToString());

            }
        }
        public static void Update(Book book)
        {
            using (var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(book);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PutAsync(url + "/" + book.Id, content).Result;

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
