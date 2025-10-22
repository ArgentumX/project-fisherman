namespace Application.Interfaces.Repositories.abstraction
{
    public interface ISingletonRepository<T>
    {
        T GetInstance();
        void Save(T entity);
    }
}