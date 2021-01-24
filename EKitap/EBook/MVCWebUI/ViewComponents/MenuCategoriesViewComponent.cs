using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using MVCWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebUI.ViewComponents
{
    [ViewComponent]
    public class MenuCategoriesViewComponent : ViewComponent
    {
        private ICategoryService _categoryService;

        public MenuCategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = new CategoryListViewModel
            {
                Categories = _categoryService.GetListNotFirst().OrderBy(c => c.NormalizedName).ToList()
            };
            return View(model);

        }
    }
}
