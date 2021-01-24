using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCWebUI.Models;

namespace MVCWebUI.Controllers
{
    // DELETE FROM TABLENAME
    //DBCC CHECKIDENT('DATABASENAME.dbo.TABLENAME', RESEED, 0)
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private IBookAuthorService _bookAuthorService;
        private IBookCategoryService _bookCategoryService;
        private IBookImageService _bookImageService;
        private IAuthorService _authorService;

        public HomeController(ILogger<HomeController> logger,IBookService bookService, ICategoryService categoryService, IBookCategoryService bookCategoryService, IBookAuthorService bookAuthorService, IBookImageService bookImageService, IAuthorService authorService)
        {
            _logger = logger;
            _bookService = bookService;
            _categoryService = categoryService;
            _bookAuthorService = bookAuthorService;
            _bookCategoryService = bookCategoryService;
            _bookImageService = bookImageService;
            _authorService = authorService;
        }

        public IActionResult Index()
        {
            var books = _bookService.GetBooks(18);
            var listCategories = new List<BookCategory>();
            var listImages = new List<BookImage>();
            foreach (var item in books)
            {
                listCategories.AddRange(_bookCategoryService.GetListByBookId(item.Id));
                listImages.Add(_bookImageService.GetByBookId(item.Id));
            }
            var model = new HomeViewModel
            {
                Books = books,
                BookCategoriesList = listCategories,
                BooksByDisplayed = _bookService.GetBooksByDisplayed(5),
                BooksByDate = _bookService.GetBooksByDate(3),
                BookImages = listImages,
                Categories = _categoryService.GetList()
            };   
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Book(int Id)
        {
            var books = _bookService.GetBooks(6);
            var listImages = new List<BookImage>();
            foreach (var item in books)
            {
                listImages.Add(_bookImageService.GetByBookId(item.Id));
            }
            var Book = _bookService.GetById(Id);
            
            var model = new BookViewModel
            {
                Book = Book,
                BookCategoriesList = _bookCategoryService.GetListByBookId(Id),
                BookAuthorsList = _bookAuthorService.GetListByBookId(Id),
                BookImage = _bookImageService.GetByBookId(Id),
                Categories = _categoryService.GetList(),
                Author = _authorService.GetList(),
                RandomBook = books,
                RandomBooksImages =  listImages
            };
            Book.Displayed = Book.Displayed + 1;
            _bookService.Update(Book);
            return View(model);
        }

        public IActionResult Category(int Id)
        {
            var CategoriesBook = _bookCategoryService.GetListByCategoryId(Id);
            var Books = new List<Book>();
            var AuthorsBook = new List<BookAuthor>();
            var ImagesBook = new List<BookImage>();
            ViewBag.CategoryId = Id;
            foreach (var item in CategoriesBook)
            {
                Books.Add(_bookService.GetById(item.BookId));
                AuthorsBook.AddRange(_bookAuthorService.GetListByBookId(item.BookId));
                ImagesBook.Add(_bookImageService.GetByBookId(item.BookId));
            }
            var model = new HomeViewModel
            {
                BookCategoryById = CategoriesBook,
                Books = Books,
                BookAuthorsList = AuthorsBook,
                BookImages = ImagesBook,
                Category = _categoryService.GetById(Id) ,
                Authors = _authorService.GetList()
            };
            return View(model);
        }

        public IActionResult Author(int Id)
        {
            var AuthorsBook = _bookAuthorService.GetListByAuthorId(Id);
            var Books = new List<Book>();
            var ImagesBook = new List<BookImage>();
            ViewBag.AuthorId = Id;
            foreach (var item in AuthorsBook)
            {
                Books.Add(_bookService.GetById(item.BookId));
                ImagesBook.Add(_bookImageService.GetByBookId(item.BookId));
            }
            var model = new HomeViewModel
            {
                BookAuthorById = AuthorsBook,
                Books = Books,
                BookImages = ImagesBook,
                Author = _authorService.GetById(Id)
            };
            return View(model);
        }
    }
}
