using System;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace BibliotecaUdeA.Business.DependencyInjection
{
    public static class ServicesLocator
    {
        private static IKernel kernel;

        public static void ConfigureKernel(NinjectModule[] modules)
        {
            if (kernel == null)
            {
                kernel = new StandardKernel(modules);
            }
        }

        public static void Inject(object target)
        {
            if (kernel == null)
            {
                throw new InvalidOperationException("El Kernel no está configurado.");
            }

            kernel.Inject(target);
        }

        public static T Get<T>(IParameter[] parameters = null)
        {
            if (kernel == null)
            {
                throw new InvalidOperationException("El Kernel no está configurado.");
            }

            if (parameters == null)
            {
                return kernel.Get<T>();
            }
            else
            {
                return kernel.Get<T>(parameters);
            }
        }
    }
}
