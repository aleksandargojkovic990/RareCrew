using RareCrew_CSharp.Broker;
using RareCrew_CSharp.Models;

namespace RareCrew_CSharp.Storage.Implementation
{
    public class GenericApiRepository : IGenericApiRepository
    {
        private ApiBroker apiBroker;

        public GenericApiRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            apiBroker = new ApiBroker(httpClientFactory, configuration);
        }

        public List<T> Get<T>(IEntity entity) where T : IEntity
        {
            return apiBroker.Get<T>(entity);
        }

        public void OpenConnection()
        {
            apiBroker.OpenConnection();
        }

        public void CloseConnection()
        {
            apiBroker.CloseConnection();
        }

        public void Commit()
        {
            apiBroker.Commit();
        }

        public void Rollback()
        {
            apiBroker.Rollback();
        }
    }
}
