using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryApi.BusinesLayer.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
    }
}
