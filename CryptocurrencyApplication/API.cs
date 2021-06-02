using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace CryptocurrencyApp
{
    class API
    {
        private static string API_KEY = "f0f5f963-778c-4cf8-a17f-de8c8054288a";
        private static string[] coins = new string[] { "Bitcoin", "Ethereum", "Litecoin", "IOTA", "Neo", "Cardano", "Stellar", "Monero", "Dogecoin" };
        private static dynamic[] neededCoins = new dynamic[9];


        public dynamic[] getNeededCoins()
        {
            return neededCoins;
        }

        public void updateData()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", API_KEY);
            var response = httpClient.GetAsync("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest").Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine($"Failed req {response.StatusCode}");
            }

            string responseMessage = response.Content.ReadAsStringAsync().Result;
            dynamic deserializedValue = JsonConvert.DeserializeObject(responseMessage);
            var values = deserializedValue["data"];

            for (int i = 0; i < coins.Length; i++)
            {
                for (int j = 0; j < values.Count; j++)
                {
                    string name = values[j]["name"];

                    if (String.Equals(coins[i], name))
                    {
                        neededCoins[i] = values[j];
                    }
                }
            }
        }
        public API()
        {
            updateData();
        }
        
    }
}
