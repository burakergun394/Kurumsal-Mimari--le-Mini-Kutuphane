using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthorService
    {
        void Add(Author author);
        void Delete(Author author);
        void Update(Author author);
        List<Author> GetList();
        List<Author> GetListNotFirst();
        bool CheckName(string normalizedName);
        Author GetById(int Id);

    }
}
