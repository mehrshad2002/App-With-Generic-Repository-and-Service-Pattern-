using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository<T>
    {
        bool Add(T obj );
        bool Update(T obj);
        bool Delete(int id);
        List<T> ReadAll();
        T ReadByID(int id );
    }
}
