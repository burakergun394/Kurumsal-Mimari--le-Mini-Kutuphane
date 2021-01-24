using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Business.Concrete
{
    public class BookManager : IBookService
    {
        private IBookDal _bookdal;
        
        public BookManager(IBookDal bookDal)
        {
            _bookdal = bookDal;
        }
        public void Add(Book book)
        {
            _bookdal.Add(book);
        }

        public void Delete(Book book)
        {
            _bookdal.Delete(book);
        }

        public Book GetById(int Id)
        {
            return _bookdal.Get(c => c.Id == Id);
        }

        public List<Book> GetList()
        {
            return _bookdal.GetList();
        }

        public void Update(Book book)
        {
            _bookdal.Update(book);
        }

        public List<Book> GetBooks(int limit)
        {
            var listNumber = new List<int>();
            Random rand = new Random();
            int max = GetBookId().Count;
            int number = rand.Next(0, max);
            listNumber.Add(number);
            do
            {
                number = rand.Next(0, max);
                if (!listNumber.Contains(number))
                {
                    listNumber.Add(number);
                }
                if (listNumber.Count == limit)
                {
                    break;
                }
            } while (true);

            var books = new List<Book>();
            var allbooks = GetList();
        
            foreach (var item in listNumber)
            {
                books.Add(allbooks[item]);
            }

            return books;
        }

        public List<int> GetBookId()
        {
            List<int> bookIds = new List<int>();
            var books = _bookdal.GetList();
            foreach(var item in books)
            {
                bookIds.Add(item.Id);
            }
            return bookIds;
        }

        public List<Book> GetBooksByDate(int limit)
        {
            return _bookdal.GetList().OrderByDescending(c => c.Date).Take(limit).ToList();
        }

        public List<Book> GetBooksByDisplayed(int limit)
        {
            return _bookdal.GetList().OrderByDescending(c => c.Displayed).Take(limit).ToList();
        }

        
    }
}

