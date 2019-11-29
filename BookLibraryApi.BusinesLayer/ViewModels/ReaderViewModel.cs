using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryApi.BusinesLayer.ViewModels
{
    public class ReaderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> BookIds { get; set; }
    }
}
