using RareCrew_CSharp.Models;

namespace RareCrew_CSharp.Storage
{
    public interface IGenericApiRepository
    {
        void OpenConnection();
        List<T> Get<T>(IEntity entity) where T : IEntity;
        void CloseConnection();
        void Commit();
        void Rollback();
    }
}
