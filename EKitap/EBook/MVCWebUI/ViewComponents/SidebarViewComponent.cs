using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using MVCWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebUI.ViewComponents
{
    [ViewComponent]
    public class SidebarViewComponent : ViewComponent
    {
        private IBookService _bookService;
        private IBookImageService _bookImageService;

        public SidebarViewComponent(IBookService bookService, IBookImageService bookImageService)
        {
            _bookService = bookService;
            _bookImageService = bookImageService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var BookDate = new List<BookImage>();
            var BookDisplayed = new List<BookImage>();

            var model = new HomeViewModel
            {
                BooksByDate = _bookService.GetBooksByDate(3),
                BooksByDisplayed = _bookService.GetBooksByDisplayed(5)
            };
            foreach (var item in model.BooksByDate)
            {
                BookDate.Add(_bookImageService.GetByBookId(item.Id));
            }
            foreach (var item in model.BooksByDisplayed)
            {
                BookDisplayed.Add(_bookImageService.GetByBookId(item.Id));
            }
            ViewBag.BookByDateImages = BookDate;
            ViewBag.BookByDisplayedImages = BookDisplayed;
            return View(model);
            
        }
    }
}
