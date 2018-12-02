using System;
using BibliotecaUdeA.Business.Contracts.Repositories.Remote;
using BibliotecaUdeA.Business.Dtos;

namespace BibliotecaUdeA.DataAcces.Repositories.Remote
{
    public class BooksRepository : IBooksRepository
    {
        public BooksResponse FetchListBooksByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
