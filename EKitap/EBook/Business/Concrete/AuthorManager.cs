using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthorManager : IAuthorService
    {
        private IAuthorDal _authorDal;

        public AuthorManager(IAuthorDal authorDal)
        {
            _authorDal = authorDal;
        }
        public void Add(Author author)
        {
            _authorDal.Add(author);
        }

        public bool CheckName(string normalizedName)
        {
            Author author = _authorDal.Get(c => c.NormalizedName == normalizedName);
            if (author == null)
            {
                return false;
            }
            return true;
        }

        public void Delete(Author author)
        {
            _authorDal.Delete(author);
        }

        public Author GetById(int Id)
        {
            return _authorDal.Get(c => c.Id == Id);
        }

        public List<Author> GetList()
        {
            return _authorDal.GetList();
        }

        public List<Author> GetListNotFirst()
        {
            return _authorDal.GetList(c => c.Id > 1);
        }

        public void Update(Author author)
        {
            _authorDal.Update(author);
        }
    }
}
