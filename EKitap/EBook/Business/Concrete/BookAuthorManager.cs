using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BookAuthorManager : IBookAuthorService
    {
        private IBookAuthorDal _bookAuthorDal;

        public BookAuthorManager(IBookAuthorDal bookAuthorDal)
        {
            _bookAuthorDal = bookAuthorDal;
        }
        public void Add(int bookId, string[] bookAuthors)
        {
            foreach (var item in bookAuthors)
            {
                AddBook(new BookAuthor(), bookId, Convert.ToInt32(item));
            }
        }

        public void AddDefaultValue(int bookId)
        {
            AddBook(new BookAuthor(), bookId, 1);
        }

        public void Delete(BookAuthor bookAuthor)
        {
            _bookAuthorDal.Delete(bookAuthor);
        }

        public List<BookAuthor> GetListByAuthorId(int authorId)
        {
            return _bookAuthorDal.GetList(c => c.AuthorId == authorId);
        }

        public List<BookAuthor> GetListByBookId(int bookId)
        {
            return _bookAuthorDal.GetList(c => c.BookId == bookId);
        }
        public List<BookAuthorDetail> GetBookAuthorDetails()
        {
            return _bookAuthorDal.GetBookAuthorDetails();
        }

        public void UpdateDefaultValue(int bookId)
        {
            var bookAuthorsList = GetListByBookId(bookId);
            for (int i = 0; i < bookAuthorsList.Count; i++)
            {
                if (i == 0) { UpdateBook(bookAuthorsList[i], bookId, 1); }
                else { _bookAuthorDal.Delete(bookAuthorsList[i]); }
            }
        }

        public void Update(int bookId, string[] bookAuthors)
        {
            var bookAuthorsList = GetListByBookId(bookId);
            int newSize = bookAuthors.Length;
            int oldSize = bookAuthorsList.Count;

            if (newSize >= oldSize)
            {
                for (int i = 0; i < newSize; i++)
                {
                    if (oldSize == newSize)
                    {
                        UpdateBook(bookAuthorsList[i], bookId, Convert.ToInt32(bookAuthors[i]));
                    }
                    else if (oldSize < newSize)
                    {
                        if (i > oldSize - 1)
                        {
                            AddBook(new BookAuthor(), bookId, Convert.ToInt32(bookAuthors[i]));
                        }
                        else
                        {
                            UpdateBook(bookAuthorsList[i], bookId, Convert.ToInt32(bookAuthors[i]));
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
                        UpdateBook(bookAuthorsList[i], bookId, Convert.ToInt32(bookAuthors[i]));
                    }
                    else
                    {
                        _bookAuthorDal.Delete(bookAuthorsList[i]);
                    }
                }
            }
        }

        public void UpdateBook(BookAuthor bookAuthor, int bookId, int authorId)
        {
            bookAuthor.BookId = bookId;
            bookAuthor.AuthorId = authorId;
            _bookAuthorDal.Update(bookAuthor);
        }
        public void AddBook(BookAuthor bookAuthor, int bookId, int authorId)
        {
            bookAuthor.BookId = bookId;
            bookAuthor.AuthorId = authorId;
            _bookAuthorDal.Add(bookAuthor);
        }
    }
}
