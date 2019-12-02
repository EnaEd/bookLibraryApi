using System.Collections.Generic;

namespace BookLibraryApi.BusinesLayer.ViewModels
{
    public class ReaderViewModel
    {
        public ReaderViewModel()
        {
            Books = new List<BookViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookViewModel> Books { get; set; }
    }
}
