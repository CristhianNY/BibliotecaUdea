using System;
using System.Collections.Generic;
using BibliotecaUdeA.Business.DependencyInjection.Modules;
using Ninject.Modules;

namespace BibliotecaUdeA.Business.DependencyInjection
{
    public class DependencyInjectionManager
    {
        public void ConfigureInjection(IEnumerable<NinjectModule> platformModules)
        {
            var modulesContainer = new List<NinjectModule>();

            modulesContainer.AddRange(repositoryModules);

            if (platformModules != null)
            {
                modulesContainer.AddRange(platformModules);
            }

            ServicesLocator.ConfigureKernel(modulesContainer.ToArray());
        }

        private IEnumerable<NinjectModule> repositoryModules
        {
            get
            {
                return new List<NinjectModule>
                {
                    new RepositoriesModule()
                };
            }
        }
    }
}
