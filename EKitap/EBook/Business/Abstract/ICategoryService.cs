using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetList();
        void Add(Category category);
        bool CheckName(string normalizedName);
        Category GetById(int Id);
        void Delete(Category category);
        void Update(Category category);
        List<Category> GetListNotFirst();
    }
}
