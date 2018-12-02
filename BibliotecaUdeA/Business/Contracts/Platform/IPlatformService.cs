using System;
using System.Net.Http;

namespace BibliotecaUdeA.Business.Contracts.Platform
{
    public interface IPlatformService
    {
        string Hwid { get; }
        string AppVersion { get; }
        string DeviceModel { get; }
        HttpClientHandler PlatformHttpClientHandler();
    }
}
