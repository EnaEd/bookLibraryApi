using BookLibraryApi.DataAccess.EF;
using BookLibraryApi.DataAccess.Entities;
using BookLibraryApi.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibraryApi.DataAccess.Repositories
{
    public class ReaderRepository : IRepository<Reader>
    {
        private ApplicationContext _context;

        public ReaderRepository(ApplicationContext application)
        {
            _context = application;
        }

        public void Create(Reader item)
        {
            if (!_context.Readers.Any(reader=>reader.Id==item.Id))
            {
                _context.Readers.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(Reader item)
        {
            if (_context.Readers.Any(reader => reader.Id == item.Id))
            {
                _context.Readers.Remove(item);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Reader> Get()
        {
            return _context.Readers;
        }

        public IEnumerable<Reader> Get(string id)
        {
            return null;
        }

        public Reader Get(int Id)
        {
            return _context.Readers.FirstOrDefault(reader => reader.Id == Id);
        }

        public void Update(Reader item)
        {
            if (_context.Readers.Any(reader => reader.Id == item.Id))
            {
                _context.Readers.Update(item);
                _context.SaveChanges();
            }
        }
    }
}
