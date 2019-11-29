using BookLibraryApi.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryApi.DataAccess.Repositories
{
    public class BaseRepository<T>:IRepository<T> where T :class
    {
        private readonly IConfiguration _configuration;
        protected string _connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString= _configuration.GetConnectionString("DefaultConnection");
        }

        public void Create(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
