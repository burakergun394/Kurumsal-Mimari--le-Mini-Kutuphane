using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBookImageService
    {
        void Add(int bookId, string bookImagePath);
        void Delete(BookImage bookImage);
        void Update(int bookId, string imagePath);
        List<BookImage> GetList();
        BookImage GetByBookId(int bookId);
    }
}
