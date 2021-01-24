using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class BookCategoryDetail : IDTo
    {
        public int BookId { get; set; }
        public string CategoryName { get; set; }
    }
}
