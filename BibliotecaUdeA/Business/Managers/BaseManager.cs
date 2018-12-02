using System;
using BibliotecaUdeA.Business.Contracts.Repositories.Remote;

namespace BibliotecaUdeA.Business.Managers
{
    public class BaseManager
    {
        private readonly IBooksRepository booksRepository;
    }
}