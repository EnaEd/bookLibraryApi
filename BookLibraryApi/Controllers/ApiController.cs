﻿using BookLibraryApi.BusinesLayer.Intefaces;
using BookLibraryApi.BusinesLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class ApiController : ControllerBase
    {
        private IService<BookViewModel> _bookService;
        private IService<ReaderViewModel> _readerService;
        public ApiController(IService<BookViewModel> bookservice, IService<ReaderViewModel> readerservice)
        {
            _bookService = bookservice;
            _readerService = readerservice;
        }
        [HttpGet("GetBook")]
        public IEnumerable<BookViewModel> GetBook()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }
            return _bookService.Get();
        }

        [HttpGet("GetReader")]
        public IEnumerable<ReaderViewModel> GetReader()
        {
            return _readerService.Get();
        }
    }
}