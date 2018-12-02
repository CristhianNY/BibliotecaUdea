using System;
using BibliotecaUdeA.Business.Contracts.Repositories.Remote;
using BibliotecaUdeA.DataAcces.Repositories.Remote;
using Ninject.Modules;

namespace BibliotecaUdeA.Business.DependencyInjection.Modules
{
    public class RepositoriesModule : NinjectModule
    {
        public override void Load()
        {

#if MOCKS

#else
             Bind<IBooksRepository>().To<BooksRepository>();
#endif
        }
    }
}
