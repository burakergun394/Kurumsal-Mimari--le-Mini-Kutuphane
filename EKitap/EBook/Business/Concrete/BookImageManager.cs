using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    public class BookImageManager : IBookImageService
    {
        private IBookImageDal _bookImageDal;

        public BookImageManager(IBookImageDal bookImageDal)
        {
            _bookImageDal = bookImageDal;
        }
        public void Add(int bookId, string bookImagePath)
        {
            var bookImage = new BookImage();
            bookImage.BookId = bookId;
            bookImage.Path = bookImagePath;
            _bookImageDal.Add(bookImage);
        }

        public void Delete(BookImage bookImage)
        {
            _bookImageDal.Delete(bookImage);
        }

        public BookImage Get()
        {
            throw new NotImplementedException();
        }

        public BookImage GetByBookId(int bookId)
        {
             return _bookImageDal.Get(c => c.BookId == bookId);
        }

        public List<BookImage> GetList()
        {
            return _bookImageDal.GetList();
        }

        public void Update(int bookId, string imagePath)
        {
            var bookImage = GetByBookId(bookId);
            if (imagePath.Equals(""))
            {
                imagePath = bookImage.Path;              
            }
            bookImage.Path = imagePath;
            _bookImageDal.Update(bookImage);
        }
    }
}
