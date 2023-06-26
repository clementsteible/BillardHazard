namespace BillardHazard.Repositories
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll();
        public T? FindById(int id);
        public void Delete(int id);
        public void Delete(T entity);
        public void Update(T entity);
        public void Create(T entity);
    }
}
