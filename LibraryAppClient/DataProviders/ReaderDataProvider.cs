using LibraryAppClient.Modells;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace LibraryAppClient.DataProviders
{
    class ReaderDataProvider 
    {
        private static string url = "http://localhost:5000/api/people";

        public static IList<Reader> GetAllAsList()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var people = JsonConvert.DeserializeObject<IList<Reader>>(rawData);
                    return people;
                }
                else
                    throw new InvalidOperationException(response.StatusCode.ToString());
            }
        }
        public static Reader GetById(long id)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url + "/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var user = JsonConvert.DeserializeObject<Reader>(rawData);
                    return user;
                }
                else
                    throw new InvalidOperationException(response.StatusCode.ToString());
            }
        }
        public static void Create(Reader reader)
        {
            using(var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(reader);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content).Result;

                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException(response.StatusCode.ToString());

            }
        }
        public static void Update(Reader reader)
        {
            using (var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(reader);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PutAsync(url + "/" + reader.Id, content).Result;

                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException(response.StatusCode.ToString());

            }
        }
        public static void DeleteById(long id)
        {
            using (var client = new HttpClient())
            {

                var response = client.DeleteAsync(url + "/" + id).Result;

                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException(response.StatusCode.ToString());

            }
        }


    }
}
