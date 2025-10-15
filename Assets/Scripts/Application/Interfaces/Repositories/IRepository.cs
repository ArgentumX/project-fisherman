namespace Application.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        T Get();
        void Save(T target);
    }
    
}