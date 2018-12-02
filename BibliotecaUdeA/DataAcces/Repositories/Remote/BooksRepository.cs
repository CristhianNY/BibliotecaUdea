using System;
using System.Net.Http;
using BibliotecaUdeA.Business.Contracts.Platform;
using BibliotecaUdeA.Business.Contracts.Repositories.Remote;
using BibliotecaUdeA.Business.Dtos;

namespace BibliotecaUdeA.DataAcces.Repositories.Remote
{
    public class BooksRepository : IBooksRepository
    {
        private const string booksEndPoint = "search/{0}";
        private const string rootEndPoint = "api/mobility/v2.3/";
        private const string baseURL = "https://api.itbook.store/1.0/";

        private readonly IPlatformService platformService;
        private readonly HttpClientHandler httpClientHandler;
        private readonly URLHelper urlHelper;
        private readonly string urlBase;
        public BooksRepository(IPlatformService platformService)
        {
            urlHelper = new URLHelper();
            this.platformService = platformService;
            httpClientHandler = this.platformService.PlatformHttpClientHandler();
            urlBase = urlHelper.BuildUrl(baseURL, rootEndPoint);

        }
        public BooksResponse FetchListBooksByName(string name)
        {
            string serviceUrl = string.Format(booksEndPoint, name);
            var fullUrl = urlHelper.BuildUrl(urlBase, serviceUrl);
            var repositoryBase = new RESTConsumer<object, BooksResponse>(httpClientHandler);
            return repositoryBase.ConsumeRestService(null, fullUrl, HttpMethod.Get);
        }
    }
}
