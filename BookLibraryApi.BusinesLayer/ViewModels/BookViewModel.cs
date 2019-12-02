namespace BookLibraryApi.BusinesLayer.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int? ReaderId { get; set; }
        public ReaderViewModel Reader { get; set; }
    }
}
