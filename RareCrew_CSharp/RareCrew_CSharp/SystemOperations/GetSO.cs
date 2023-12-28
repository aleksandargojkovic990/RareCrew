using RareCrew_CSharp.Models;

namespace RareCrew_CSharp.SystemOperations
{
    public class GetSO<T> : SystemOperationBase where T : IEntity
    {
        public GetSO(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public List<T> Result { get; private set; }

        protected override void ExecuteOperation(IEntity entity)
        {
            Result = repository.Get<T>(entity);
        }
    }
}