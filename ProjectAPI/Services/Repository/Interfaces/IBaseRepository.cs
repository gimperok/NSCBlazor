namespace ProjectAPI.Services.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        List<T> GetList();

        int Add(T entity);
        bool Edit(T entity);
        bool Delete(int id);
    }
}