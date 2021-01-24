using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using MVCWebUI.Models;

namespace MVCWebUI.Controllers
{
    public class AdminController : Controller
    {
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private IAuthorService _authorService;
        private IBookAuthorService _bookAuthorService;
        private IBookCategoryService _bookCategoryService;
        private IBookImageService _bookImageService;


        public AdminController(IBookService bookService, ICategoryService categoryService, IAuthorService authorService, IBookCategoryService bookCategoryService, IBookAuthorService bookAuthorService, IBookImageService bookImageService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _authorService = authorService;
            _bookAuthorService = bookAuthorService;
            _bookCategoryService = bookCategoryService;
            _bookImageService = bookImageService;
        }
        public IActionResult Index()
        {
            var BooksDate = _bookService.GetBooksByDate(4);
            var BooksDisplayed = _bookService.GetBooksByDisplayed(1);
            var BookImagesForDate = new List<BookImage>();
            var BookImagesForDisplayed= new List<BookImage>();
   
            foreach(var item in BooksDate)
            {
                BookImagesForDate.Add(_bookImageService.GetByBookId(item.Id));
            }
            foreach(var item in BooksDisplayed)
            {
                BookImagesForDisplayed.Add(_bookImageService.GetByBookId(item.Id));
            }
            var model = new AdminIndexViewModel
            {
                BooksForDate = BooksDate,
                BookImagesForDate = BookImagesForDate,
                SumDisplayedOfBooks = _bookService.GetList().Sum(c => c.Displayed),
                SumBookofBooks = _bookService.GetList().Count,
                BooksForDisplayed = BooksDisplayed,
                BookImagesForDisplayed = BookImagesForDisplayed
            };
            return View(model);
        }
    }
}