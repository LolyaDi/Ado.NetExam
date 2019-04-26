using System;
using System.Collections.Generic;

namespace ExamWork.DataAccess
{
    public interface IRepository<T>: IDisposable
    {
        void Add(T item);

        void Update(T item);

        void Delete(Guid id);

        ICollection<T> GetAll();
    }
}
