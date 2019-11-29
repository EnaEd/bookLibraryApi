using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookLibraryApi.DataAccess.Entities
{
    public class Reader
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<int> BookIds { get; set; }
    }
}
