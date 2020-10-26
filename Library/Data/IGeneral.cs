using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Data
{
    public interface IGeneral<T>
    {
        int Add(T item);
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Delete(int id);

        void Update(int id);
    }
}
