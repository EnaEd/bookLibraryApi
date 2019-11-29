using BookLibraryApi.DataAccess.EF;
using BookLibraryApi.DataAccess.Entities;
using BookLibraryApi.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BookLibraryApi.DataAccess.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private ApplicationContext _context;

        public BookRepository(ApplicationContext application)
        {
            _context = application;
        }

        public void Create(Book item)
        {
            if (!_context.Books.Any(book => book.Id == item.Id))
            {
                _context.Books.AddAsync(item);
                _context.SaveChangesAsync();
            }
        }

        public void Delete(Book item)
        {
            if (_context.Books.Any(book => book.Id == item.Id))
            {
                _context.Books.Remove(item);
                _context.SaveChangesAsync();
            }
        }

        public IEnumerable<Book> Get()
        {
            return _context.Books;
        }


        public IEnumerable<Book> Get(string category)
        {
            return _context.Books.Where(book => book.Category.Equals(category));
        }

        public void Update(Book item)
        {
            if (_context.Books.Any(book => book.Id == item.Id))
            {
                _context.Books.Update(item);
                _context.SaveChangesAsync();
            }
        }

        Book IRepository<Book>.Get(int Id)
        {
            return _context.Books.FirstOrDefault(book => book.Id == Id);
        }
    }
}
