using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebUI.Models
{
    public class BookAddViewModel
    {
        public Book Book { get; set; }
        public List<Category> Categories { get; set; }
        public List<Author> Author { get; set; }
        public string[] BookCategories { get; set; }
        public string[] BookAuthors { get; set; }
    }
}
