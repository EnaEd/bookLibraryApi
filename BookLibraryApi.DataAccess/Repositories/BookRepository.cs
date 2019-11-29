using BookLibraryApi.DataAccess.Entities;
using BookLibraryApi.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BookLibraryApi.DataAccess.Repositories
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
