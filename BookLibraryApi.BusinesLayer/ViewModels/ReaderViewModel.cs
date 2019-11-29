using System.Collections.Generic;

namespace BookLibraryApi.BusinesLayer.ViewModels
{
    public class ReaderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> BookIds { get; set; }
    }
}
