using System;
using System.Net.Http;
using BibliotecaUdeA.Business.Contracts.Platform;
using BibliotecaUdeA.Enumerations;
using Android.App;
using Android.OS;
using Android.Provider;
using System.Linq;
using System;
using Android.Widget;
using static Android.Provider.Settings;
using System.Globalization;
using BibliotecaUdeA.Droid.Components;

namespace BibliotecaUdeA.Droid.DependenctInjection.Implementation
{
    public class PlatformService : IPlatformService
    {
        public PlatformKind Platform => PlatformKind.Android;
        public string AppVersion => Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, 0).VersionName;
       
        public CultureInfo CurrentCulture => new CultureInfo("es-CO");

        public string Hwid => Secure.GetString(Application.Context.ContentResolver, Secure.AndroidId);

        public string DeviceModel => string.Format("{0}, {1}, {2}", Build.Model, Build.VERSION.SdkInt, string.Join(", ", Build.SupportedAbis));

        public HttpClientHandler PlatformHttpClientHandler()
        {
            return new CustomDelegatingHandler();
        }
    }
}
