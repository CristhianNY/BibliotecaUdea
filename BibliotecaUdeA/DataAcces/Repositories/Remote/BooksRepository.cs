using System;
using BibliotecaUdeA.Business.Contracts.Platform;
using BibliotecaUdeA.Business.Contracts.Repositories.Remote;
using BibliotecaUdeA.Business.Dtos;

namespace BibliotecaUdeA.DataAcces.Repositories.Remote
{
    public class BooksRepository : IBooksRepository
    {
        private const string dasboardEndPoint = "insights/score/{0}/{1}";

        private readonly IPlatformService platformService;
        private readonly URLHelper urlHelper;
        private readonly string urlBase;
        public BooksRepository(IPlatformService platformService)
        {

        }
        public BooksResponse FetchListBooksByName(string name)
        {

            return null;
        }
    }
}
