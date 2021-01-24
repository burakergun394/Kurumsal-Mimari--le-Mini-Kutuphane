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
    public class EfBookCategory : EfEntityRepositoryBase<BookCategory, EBookContext>, IBookCategoryDal
    {
        public List<BookCategoryDetail> GetBookCategoryDetails()
        {
            using (EBookContext context = new EBookContext())
            {

                var result = (from bc in context.BookCategories.AsEnumerable()
                              join b in context.Books on bc.BookId equals b.Id
                              join c in context.Categories on bc.CategoryId equals c.Id
                              group c by new { b.Id } into g
                              select new BookCategoryDetail
                              {
                                  BookId = g.Key.Id,
                                  CategoryName = string.Join(",", g.Select(x => x.Name).ToList())
                              });

                return result.ToList();
            };

        }
    }
}
