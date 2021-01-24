using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebUI.Models
{
    public class HomeViewModel
    {
        public List<Book> Books { get; set; }
        public List<Category> Categories { get; set; }
        public Category Category { get; set; }
        public List<Author> Authors { get; set; }
        public Author Author { get; set; }
        public List<BookCategory> BookCategoriesList { get; set; }
        public List<BookAuthor> BookAuthorsList { get; set; }
        public List<BookImage> BookImages { get; set; }
        public string[] BookCategories { get; set; }
        public string[] BookAuthors { get; set; }
        public List<Book> BooksByDate { get; set; }
        public List<Book> BooksByDisplayed { get; set; }
        public List<BookCategory> BookCategoryById { get; set; }
        public List<BookAuthor> BookAuthorById { get; set; }
    }
}
