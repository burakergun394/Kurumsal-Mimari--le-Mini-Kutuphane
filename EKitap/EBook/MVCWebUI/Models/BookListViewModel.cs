using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebUI.Models
{
    public class BookListViewModel
    {
        public List<Book> Books { get; set; }
        public List<BookCategoryDetail> BookCategoryDetails { get; set; }
        public List<BookAuthorDetail> BookAuthorDetails { get; set; }
        public List<BookImage> BookImages { get; set; }
    }
}
