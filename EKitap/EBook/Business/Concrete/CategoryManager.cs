using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Add(Category category)
        {
            _categoryDal.Add(category);
        }

        public List<Category> GetList()
        {
            return _categoryDal.GetList();
        }

        public bool CheckName(string normalizedName)
        {
            Category category = _categoryDal.Get(c => c.NormalizedName == normalizedName);
            if (category == null)
            {
                return false;
            }
            return true;
        }

        public Category GetById(int Id)
        {
            return _categoryDal.Get(c => c.Id == Id);
        }

        public void Delete(Category category)
        {
           _categoryDal.Delete(category);
        }

        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }

        public List<Category> GetListNotFirst()
        {
            return _categoryDal.GetList(c => c.Id > 1);
        }
    }
}
