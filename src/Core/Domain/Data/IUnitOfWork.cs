namespace Core.Domain.Data
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}