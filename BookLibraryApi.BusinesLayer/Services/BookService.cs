using AutoMapper;
using BookLibraryApi.BusinesLayer.Intefaces;
using BookLibraryApi.BusinesLayer.ViewModels;
using BookLibraryApi.DataAccess.Entities;
using BookLibraryApi.DataAccess.Interfaces;
using System.Collections.Generic;

namespace BookLibraryApi.BusinesLayer.Services
{
    public class BookService : BaseService, IService<BookViewModel>
    {
        private readonly IRepository<Book> _bookRepository;
        public BookService(IRepository<Book> bookRepository,
            IMapper mapper) : base(mapper)
        {
            _bookRepository = bookRepository;
        }

        //implement extra CRUD logic here
        public void Create(BookViewModel book)
        {
            Book mapped = _mapper.Map<Book>(book);
            _bookRepository.Create(mapped);
        }

        public void Delete(BookViewModel book)
        {
            Book mapped = _mapper.Map<Book>(book);
            _bookRepository.Delete(mapped);
        }

        public void Update(BookViewModel book)
        {
            Book mapped = _mapper.Map<Book>(book);
            _bookRepository.Update(mapped);
        }

        public IEnumerable<BookViewModel> Get()
        {
            IEnumerable<BookViewModel> books = _mapper.Map<IEnumerable<BookViewModel>>(_bookRepository.Get());
            return books;
        }

        public IEnumerable<BookViewModel> Get(string category)
        {
            IEnumerable<BookViewModel> books = _mapper.Map<IEnumerable<BookViewModel>>(_bookRepository.Get(category));
            return books;
        }

        public BookViewModel Get(int Id)
        {
            return null;
        }
    }
}
