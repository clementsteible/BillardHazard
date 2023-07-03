namespace BillardHazard.Repositories
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll();
        public T? FindById(Guid id);
        public void Delete(Guid id);
        public void Delete(T entity);
        public void Update(T entity);
        public void Create(T entity);
    }
}
