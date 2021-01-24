using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWebUI.Models;

namespace MVCWebUI.Controllers
{
    public class BookController : Controller
    {
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private IAuthorService _authorService;
        private IBookAuthorService _bookAuthorService;
        private IBookCategoryService _bookCategoryService;
        private IBookImageService _bookImageService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookService bookService, ICategoryService categoryService, IAuthorService authorService, IBookCategoryService bookCategoryService, IBookAuthorService bookAuthorService, IBookImageService bookImageService, IWebHostEnvironment webHostEnvironment)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _authorService = authorService;
            _bookAuthorService = bookAuthorService;
            _bookCategoryService = bookCategoryService;
            _bookImageService = bookImageService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var model = new BookListViewModel
            {
                Books = _bookService.GetList(),
                BookCategoryDetails = _bookCategoryService.GetBookCategoryDetails(),
                BookAuthorDetails = _bookAuthorService.GetBookAuthorDetails(),
                BookImages = _bookImageService.GetList()
            };
            return View(model);
        }

        public IActionResult Add()
        {
            var model = new BookAddViewModel
            {
                Book = new Book(),
                Categories = _categoryService.GetListNotFirst(),
                Author = _authorService.GetListNotFirst()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookAddViewModel model, IFormFile bookImage)
        {     
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(model.Book.Name) && !String.IsNullOrEmpty(model.Book.Summary))
                {
                    model.Book.Date = DateTime.Now;
                    model.Book.Displayed = 0;
                    _bookService.Add(model.Book);
                    
                    if (model.BookCategories == null) { _bookCategoryService.AddDefaultValue(model.Book.Id); }
                    else {  _bookCategoryService.Add(model.Book.Id, model.BookCategories); }

                    if (model.BookAuthors == null) { _bookAuthorService.AddDefaultValue(model.Book.Id); }
                    else { _bookAuthorService.Add(model.Book.Id, model.BookAuthors); }

                    string imagePath = "";
                    if (bookImage != null && bookImage.Length > 0)
                    {
                        var BookImage = new BookImage();
                        var filename = Guid.NewGuid().ToString() + Path.GetExtension(bookImage.FileName);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/book/", filename);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await bookImage.CopyToAsync(stream);
                            imagePath = "/img/book/" + filename;
                        }
                    }
                    if (imagePath.Equals("")) { imagePath = "/img/book/kitap-resim-yok.png"; }
                    _bookImageService.Add(model.Book.Id, imagePath);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int Id)
        {
            var model = new BookViewModel
            {
                Book = _bookService.GetById(Id),
                Categories = _categoryService.GetListNotFirst(),
                Author = _authorService.GetListNotFirst(),
                BookCategoriesList = _bookCategoryService.GetListByBookId(Id),
                BookAuthorsList = _bookAuthorService.GetListByBookId(Id),
                BookImage = _bookImageService.GetByBookId(Id)
            };
            var selectedCategoriesList = new List<int>();
            foreach (var item in model.BookCategoriesList)
            {
                selectedCategoriesList.Add(Convert.ToInt32(item.CategoryId));
            }
            ViewBag.SelectedCategories = selectedCategoriesList.ToArray();

            var selectedAuthorsList = new List<int>();
            foreach(var item in model.BookAuthorsList)
                    {
                selectedAuthorsList.Add(Convert.ToInt32(item.AuthorId));
            }
            ViewBag.SelectedAuthors = selectedAuthorsList.ToArray();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BookViewModel model, IFormFile bookImage)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(model.Book.Name) && !String.IsNullOrEmpty(model.Book.Summary))
                {            
                    _bookService.Update(model.Book);

                    if (model.BookCategories == null) { _bookCategoryService.UpdateDefaultValue(model.Book.Id); }
                    else { _bookCategoryService.Update(model.Book.Id, model.BookCategories); }

                    if (model.BookAuthors == null) { _bookAuthorService.UpdateDefaultValue(model.Book.Id); }
                    else { _bookAuthorService.Update(model.Book.Id, model.BookAuthors); }

                    string imagePath = "";
                    if (bookImage != null && bookImage.Length > 0)
                    {
                        var BookImage = new BookImage();
                        var filename = Guid.NewGuid().ToString() + Path.GetExtension(bookImage.FileName);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/book/", filename);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await bookImage.CopyToAsync(stream);
                            imagePath = "/img/book/" + filename;
                        }
                    }
                    _bookImageService.Update(model.Book.Id, imagePath);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            if (ModelState.IsValid)
            {
                if (Id > 0)
                {
                    var book = _bookService.GetById(Id);
                    var bookCategories = _bookCategoryService.GetListByBookId(Id);
                    var bookAuthors = _bookAuthorService.GetListByBookId(Id);
                    var bookImages = _bookImageService.GetByBookId(Id);
                    foreach (var item in bookCategories)
                    {
                        _bookCategoryService.Delete(item);
                    }
                    foreach (var item in bookAuthors)
                    {
                        _bookAuthorService.Delete(item);
                    }
                    _bookImageService.Delete(bookImages);
                    _bookService.Delete(book);
                    
                }
            }
            return RedirectToAction("Index");
        }
    }
}