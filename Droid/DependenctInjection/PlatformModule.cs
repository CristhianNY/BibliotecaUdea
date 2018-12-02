using System;
using BibliotecaUdeA.Business.Contracts.Platform;
using BibliotecaUdeA.Droid.DependenctInjection.Implementation;
using Ninject.Modules;

namespace BibliotecaUdeA.Droid.DependenctInjection
{
    public class PlatformModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlatformService>().To<PlatformService>();
          //  Bind<INetworkManager>().To<NetworkManager>();

        }
    }
}
