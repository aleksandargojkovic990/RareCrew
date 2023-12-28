using RareCrew_CSharp.Models;
using RareCrew_CSharp.Storage;
using RareCrew_CSharp.Storage.Implementation;

namespace RareCrew_CSharp.SystemOperations
{
    public abstract class SystemOperationBase
    {
        protected IGenericApiRepository repository;

        public SystemOperationBase(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            repository = new GenericApiRepository(httpClientFactory, configuration);
        }

        public void ExecuteTemplate(IEntity entity)
        {
            try
            {
                repository.OpenConnection();
                ExecuteOperation(entity);
                repository.Commit();
            }
            catch (Exception)
            {
                repository.Rollback();
                throw;
            }
            finally
            {
                repository.CloseConnection();
            }
        }

        protected abstract void ExecuteOperation(IEntity entity);
    }
}
