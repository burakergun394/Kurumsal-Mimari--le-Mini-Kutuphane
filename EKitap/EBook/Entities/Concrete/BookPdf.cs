using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class BookPdf : IEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Path { get; set; }
    }
}
