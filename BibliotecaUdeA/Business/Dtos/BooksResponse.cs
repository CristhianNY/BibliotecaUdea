using System;
using System.Collections.Generic;

namespace BibliotecaUdeA.Business.Dtos
{
    public class BooksResponse
    {
        public string Total { get; set; }
        public List<BookItem> Books { get; set; }
    }
}
