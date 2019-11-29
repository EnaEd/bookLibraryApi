﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryApi.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
