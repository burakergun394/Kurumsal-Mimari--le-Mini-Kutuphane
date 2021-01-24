using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BookCategoryManager : IBookCategoryService
    {
        private IBookCategoryDal _bookCategoryDal;
        public BookCategoryManager (IBookCategoryDal bookCategoryDal)
        {
            _bookCategoryDal = bookCategoryDal;
        }
        public void Add(int bookId, string [] bookCategories)
        {
            foreach(var item in bookCategories)
            {
                AddBook(new BookCategory(), bookId, Convert.ToInt32(item));     
            }
        }
        public void AddDefaultValue(int bookId)
        {
            AddBook(new BookCategory(), bookId, 1);
        }
        public void Delete(BookCategory bookCategory)
        {
            _bookCategoryDal.Delete(bookCategory);
        }
        public List<BookCategory> GetListByBookId(int bookId)
        {
            return _bookCategoryDal.GetList(c => c.BookId == bookId);
        }
        public List<BookCategory> GetListByCategoryId(int categoryId)
        {
            return _bookCategoryDal.GetList(c => c.CategoryId == categoryId);
        }
        public List<BookCategoryDetail> GetBookCategoryDetails()
        {
            return _bookCategoryDal.GetBookCategoryDetails();
        }
        public void UpdateDefaultValue(int bookId)
        {
            var bookCategoriesList = GetListByBookId(bookId);
            for (int i = 0; i < bookCategoriesList.Count; i++)
            {
                if (i == 0) { UpdateBook(bookCategoriesList[i], bookId, 1); }
                else { _bookCategoryDal.Delete(bookCategoriesList[i]); }
            }     
        }
        public void Update(int bookId, string[] bookCategories)
        {
            var bookCategoriesList = GetListByBookId(bookId);
            int newSize = bookCategories.Length;
            int oldSize = bookCategoriesList.Count;

            if (newSize >= oldSize)
            {
                for (int i = 0; i < newSize; i++)
                {
                    if (oldSize == newSize)
                    {
                        UpdateBook(bookCategoriesList[i], bookId, Convert.ToInt32(bookCategories[i]));
                    }
                    else if (oldSize < newSize)
                    {
                        if (i > oldSize - 1)
                        {     
                            AddBook(new BookCategory(), bookId, Convert.ToInt32(bookCategories[i]));
                        }
                        else
                        {
                            UpdateBook(bookCategoriesList[i], bookId, Convert.ToInt32(bookCategories[i]));
                        }
                    }                   
                }
            }
            if (newSize < oldSize)
            {
                for (int i = 0; i < oldSize; i++)
                {
                    if (i < newSize) 
                    {
                        UpdateBook(bookCategoriesList[i], bookId, Convert.ToInt32(bookCategories[i]));
                    }
                    else
                    {
                        _bookCategoryDal.Delete(bookCategoriesList[i]);
                    }
                }
            }           
        }
        public void UpdateBook(BookCategory bookCategory,int bookId, int categoryId)
        {
            bookCategory.BookId = bookId;
            bookCategory.CategoryId = categoryId;
            _bookCategoryDal.Update(bookCategory);
        }
        public void AddBook(BookCategory bookCategory, int bookId, int categoryId)
        {
            bookCategory.BookId = bookId;
            bookCategory.CategoryId = categoryId;
            _bookCategoryDal.Add(bookCategory);
        }
    }
}
