using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebUI.Models
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public List<Category> Categories { get; set; }
        public List<Author> Author { get; set; }
        public List<BookCategory> BookCategoriesList { get; set; }
        public List<BookAuthor> BookAuthorsList { get; set; }
        public List<Book> RandomBook { get; set; }
        public List<BookImage> RandomBooksImages { get; set; }
        public BookImage BookImage { get; set; }
        public string[] BookCategories { get; set; }
        public string[] BookAuthors { get; set; }
    }
}
