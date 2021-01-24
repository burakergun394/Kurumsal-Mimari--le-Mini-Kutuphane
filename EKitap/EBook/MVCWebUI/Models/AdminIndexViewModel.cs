using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebUI.Models
{
    public class AdminIndexViewModel
    {
        public List<Book> BooksForDate { get; set; }
        public List<Book> BooksForDisplayed { get; set; }
        public List<BookCategory> BookCategories { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<BookImage> BookImagesForDate { get; set; }
        public List<BookImage> BookImagesForDisplayed { get; set; }
        public int SumDisplayedOfBooks { get; set; }
        public int SumBookofBooks { get; set; }
    }
}
