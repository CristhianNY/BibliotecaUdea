using System;
using BibliotecaUdeA.Business.Dtos;

namespace BibliotecaUdeA.Business.Contracts.Repositories.Remote
{
    public interface IBooksRepository
    {
        BooksResponse FetchListBooksByName(string name);
    }
}
