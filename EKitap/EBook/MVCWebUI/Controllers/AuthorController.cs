using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using MVCWebUI.Extensions;
using MVCWebUI.Models;

namespace MVCWebUI.Controllers
{
    public class AuthorController : Controller
    {
        private IAuthorService _authorService;
        private IBookAuthorService _bookAuthorService;

        public AuthorController(IAuthorService authorService, IBookAuthorService bookAuthorService)
        {
            _authorService = authorService;
            _bookAuthorService = bookAuthorService;
        }
        public IActionResult Index()
        {
            var model = new AuthorListViewModel
            {
                Authors = _authorService.GetList()
            };
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Author author)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(author.Name))
                {
                    string normalizedName = author.Name.ToUpper();
                    author.Name = StringExtensions.FirstCharToUpper(author.Name);
                    author.NormalizedName = normalizedName;
                    if (_authorService.CheckName(normalizedName))
                    {
                        ModelState.AddModelError("", $"{author.Name} adı altında bir yazar mevcuttur.");
                    }
                    else
                    {
                        _authorService.Add(author);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Yazar kişisini girmek zorundasınız.");
                }
            }
            return View();
        }

        public IActionResult Update(int Id)
        {
            var model = new AuthorViewModel
            {
                Author = _authorService.GetById(Id)
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Author author)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(author.Name))
                {
                    string normalizedName = author.Name.ToUpper();
                    author.Name = StringExtensions.FirstCharToUpper(author.Name);
                    author.NormalizedName = normalizedName;
                    if (_authorService.CheckName(normalizedName))
                    {
                        ModelState.AddModelError("", $"{author.Name} adı altında bir yazar mevcuttur.");
                    }
                    else
                    {
                        _authorService.Update(author);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Yazar kişisini girmek zorundasınız.");
                }
            }
            return View();
        }
        public IActionResult Delete(int Id)
        {
            if (ModelState.IsValid)
            {
                if (Id > 1)
                {
                    var author = _authorService.GetById(Id);
                    var bookAuthors = _bookAuthorService.GetListByAuthorId(Id);
                    foreach(var item in bookAuthors)
                    {
                        _bookAuthorService.Delete(item);
                    }
                    _authorService.Delete(author);
                }
            }
            return RedirectToAction("Index");
        }
    }
}