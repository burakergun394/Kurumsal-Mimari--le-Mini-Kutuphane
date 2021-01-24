using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBookAuthorService
    {
        void Add(int bookId, string[] bookAuthors);
        List<BookAuthor> GetListByBookId(int bookId);
        List<BookAuthor> GetListByAuthorId(int authorId);
        void Delete(BookAuthor bookAuthor);
        void AddDefaultValue(int bookId);
        List<BookAuthorDetail> GetBookAuthorDetails();
        void UpdateDefaultValue(int bookId);
        void Update(int bookId, String[] bookAuthors);
    }
}
