using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBookService
    {
        void Add(Book book);
        void Delete(Book book);
        void Update(Book book);
        List<Book> GetList();
        Book GetById(int Id);
        List<Book> GetBooks(int limit);
        List<Book> GetBooksByDate(int limit);
        List<Book> GetBooksByDisplayed(int limit);

        
    }
}
