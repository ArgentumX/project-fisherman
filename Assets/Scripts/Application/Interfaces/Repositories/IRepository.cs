namespace Application.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        T Load();
        void Save(T target);
    }
    
}