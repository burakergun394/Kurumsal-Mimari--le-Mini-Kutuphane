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
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        private IBookCategoryService _book_categoryService;

        public CategoryController(ICategoryService categoryService, IBookCategoryService book_categoryService)
        {
            _categoryService = categoryService;
            _book_categoryService = book_categoryService;
        }
        public IActionResult Index()
        {
            var model = new CategoryListViewModel
            {
                Categories = _categoryService.GetList()
            };
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(category.Name))
                {
                    string normalizedName = category.Name.ToUpper();
                    category.NormalizedName = normalizedName;
                    category.Name = StringExtensions.FirstCharToUpper(category.Name);
                    if (_categoryService.CheckName(normalizedName))
                    {
                        ModelState.AddModelError("", $"{category.Name} adı altında bir kategori mevcuttur.");
                    }
                    else
                    {
                        _categoryService.Add(category);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kategori adını girmek zorundasınız.");
                }
            }
            return View();
        }

        public IActionResult Update(int Id)
        {
            var model = new CategoryViewModel
            {
                Category = _categoryService.GetById(Id)
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(category.Name))
                {
                    string normalizedName = category.Name.ToUpper();
                    category.NormalizedName = normalizedName;
                    category.Name = StringExtensions.FirstCharToUpper(category.Name);
                    if (_categoryService.CheckName(normalizedName))
                    {
                        ModelState.AddModelError("", $"{category.Name} adı altında bir kategori mevcuttur.");
                    }
                    else
                    {
                        _categoryService.Update(category);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kategori adını girmek zorundasınız.");
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
                    var category = _categoryService.GetById(Id);
                    var bookCategories = _book_categoryService.GetListByCategoryId(Id);
                    foreach (var item in bookCategories)
                    {
                        _book_categoryService.Delete(item);
                    }
                    _categoryService.Delete(category);
                }
            }
            return RedirectToAction("Index");
        }

        
    }
}