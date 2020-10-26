using System;
using System.Collections.Generic;
using Library.Models;

namespace Library.Data
{
    public interface ICRUD<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
