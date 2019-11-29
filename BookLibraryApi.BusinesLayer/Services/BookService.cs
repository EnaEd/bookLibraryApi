using AutoMapper;
using BookLibraryApi.BusinesLayer.ViewModels;
using BookLibraryApi.DataAccess.EF;
using BookLibraryApi.DataAccess.Entities;
using BookLibraryApi.DataAccess.Interfaces;
using BookLibraryApi.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryApi.BusinesLayer.Services
{
    public class BookService:BaseService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Reader> _readeRrepository;
        public BookService(IRepository<Book> bookRepository, IRepository<Reader> readerRepository,
            IMapper mapper):base(mapper)
        {
            _bookRepository = bookRepository;
            _readeRrepository = readerRepository;
        }
        public void Create(BookViewModel book)
        {

        }
        
        public void Delete(BookViewModel book)
        {

        }
        public void Update(BookViewModel book)
        {

        }
        public IEnumerable<BookViewModel> Get()
        {
            throw new Exception();
        }
        public IEnumerable<BookViewModel>Get(string category)
        {
            throw new Exception();
        }
    }
}
