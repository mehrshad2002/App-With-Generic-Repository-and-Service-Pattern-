using Calsses;
using Repositories;

namespace Services
{
    public class Service<T> : IService<T>
    {
        public IRepository<T> repository;

        public Service(IRepository<T> repositoryy)
        {
            this.repository = repositoryy;
        }

        public bool Add(T obj)
        {
            return repository.Add(obj);
        }

        public bool Delete(int id)
        {
            return repository.Delete(id);   
        }

        public List<T> ReadAll()
        {
            return repository.ReadAll();
        }

        public T ReadByID(int id)
        {
            return repository.ReadByID(id);
        }

        public bool Update(T obj)
        {
            return repository.Update(obj);
        }
    }
}