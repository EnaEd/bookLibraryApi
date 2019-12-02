using BookLibraryApi.DataAccess.EF;
using BookLibraryApi.DataAccess.Entities;
using BookLibraryApi.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
            if (!_context.Books.Any())
            {
                var reader1 = new Reader { Name = "Ed Ena" };
                var reader2 = new Reader { Name = "Jhon Dow" };
                _context.Readers.Add(reader1);
                _context.Readers.Add(reader2);
                _context.SaveChanges();

                _context.Books.Add(new Book { Title = "The Lord of the Rings", Author = "JRR Tolkien", Category = "Fantasy" ,Reader=reader1});
                _context.Books.Add(new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Category = "BestSeller",  Reader = reader2 });
                _context.Books.Add(new Book { Title = "His Dark Materials", Author = "Philip Pullman", Category = "BestSeller", Reader = reader2 });
                _context.Books.Add(new Book { Title = "The Hitchhiker's Guide to the Galaxy", Author = "Douglas Adams", Category = "BestSeller"});
                _context.Books.Add(new Book { Title = "Harry Potter and the Goblet of Fire", Author = "JK Rowling", Category = "Child", Reader = reader2 });
                _context.Books.Add(new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Category = "BestSeller"});
                _context.Books.Add(new Book { Title = "Winnie the Pooh", Author = "AA Milne", Category = "Child"});
                _context.Books.Add(new Book { Title = "Nineteen Eighty-Four", Author = "George Orwell", Category = "BestSeller"});
                _context.Books.Add(new Book { Title = "The Lion, the Witch and the Wardrobe", Author = "CS Lewis", Category = "BestSeller"});
                _context.Books.Add(new Book { Title = "The Hobbit", Author = "JRR Tolkien", Category = "Fantasy", Reader = reader2 });
                _context.SaveChanges();
        
            }
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
