using BillardHazard.Models;
using Microsoft.EntityFrameworkCore;

namespace BillardHazard.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BhContext _dbContext;

        public Repository(BhContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<T> GetAll()
        {            
            return _dbContext.Set<T>().ToList();
        }

        public T? FindById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Delete(int id)
        {
            T? entity = FindById(id);

            if (entity != null)
            {
                _dbContext.Remove(entity);
                _dbContext.SaveChanges();
                Console.WriteLine($"{typeof(T).Name} n°{id} supprimée !");
            }
            else
            {
                Console.WriteLine($"{typeof(T).Name} n°{id} n'existe pas !");
            }
        }

        public void Delete(T entity){ 
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
            Console.WriteLine($"{typeof(T).Name} supprimée !");
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
            Console.WriteLine($"{typeof(T).Name} modifiée !");
        }

        public void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            Console.WriteLine($"Nouvelle {typeof(T).Name} créée !");
        }
    }
}
