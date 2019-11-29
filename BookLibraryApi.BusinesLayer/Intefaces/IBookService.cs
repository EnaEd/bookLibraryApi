using System.Collections.Generic;

namespace BookLibraryApi.BusinesLayer.Intefaces
{
    public interface IService<T> where T:class
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(string id);
        T Get(int Id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
