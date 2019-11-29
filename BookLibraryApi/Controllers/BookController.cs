using BookLibraryApi.BusinesLayer.Intefaces;
using BookLibraryApi.BusinesLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private IService<BookViewModel> _bookService;
        public BookController(IService<BookViewModel> service)
        {
            _bookService = service;
        }
        [HttpGet]
        public IEnumerable<BookViewModel> Get()
        {
            return _bookService.Get();
        }
    }
}