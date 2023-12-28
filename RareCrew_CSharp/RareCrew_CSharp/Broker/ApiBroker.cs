
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RareCrew_CSharp.Models;
using System.Configuration;

namespace RareCrew_CSharp.Broker
{
    public class ApiBroker
    {
        IHttpClientFactory _httpClientFactory;
        private string apiEndpoint;
        private HttpClient? httpClient;

        public ApiBroker(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            apiEndpoint = configuration.GetConnectionString("Task1_api");
        }
        internal void OpenConnection()
        {
            httpClient = _httpClientFactory.CreateClient();
        }

        internal void CloseConnection()
        {
            if (httpClient != null)
            {
                httpClient.Dispose();
                httpClient = null;
            }
        }

        internal void Commit()
        {
        }

        internal void Rollback()
        {
        }

        public List<T> Get<T>(IEntity entity) where T: IEntity
        {
            var response = httpClient.GetStringAsync(apiEndpoint).Result;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(response);
        }
    }
}
