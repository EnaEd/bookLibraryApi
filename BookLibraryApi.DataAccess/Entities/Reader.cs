using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibraryApi.DataAccess.Entities
{
    public class Reader
    {
        public Reader()
        {
            Books = new List<Book>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<Book> Books { get; set; }
        
    }
}
