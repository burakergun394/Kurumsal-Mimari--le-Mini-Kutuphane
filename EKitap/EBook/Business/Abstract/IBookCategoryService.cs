using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBookCategoryService
    {
        void Add(int bookId, String[] bookCategories);
        void AddDefaultValue(int bookId);
        void UpdateDefaultValue(int bookId);
        void Update(int bookId, String[] bookCategories);
        List<BookCategory> GetListByBookId(int bookId);
        List<BookCategory> GetListByCategoryId(int categoryId);
        void Delete(BookCategory bookCategory);
        List<BookCategoryDetail> GetBookCategoryDetails();
    }
}
