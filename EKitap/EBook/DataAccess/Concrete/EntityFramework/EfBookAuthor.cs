using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBookAuthor : EfEntityRepositoryBase<BookAuthor, EBookContext>, IBookAuthorDal
    {
        public List<BookAuthorDetail> GetBookAuthorDetails()
        {
            using (EBookContext context = new EBookContext())
            {

                var result = (from bc in context.BookAuthors.AsEnumerable()
                              join b in context.Books on bc.BookId equals b.Id
                              join a in context.Authors on bc.AuthorId equals a.Id
                              group a by new { b.Id } into g
                              select new BookAuthorDetail
                              {
                                  BookId = g.Key.Id,
                                  AuthorName = string.Join(",", g.Select(x => x.Name).ToList())
                              });

                return result.ToList();
            };
        }
    }
}
