using System;
namespace BibliotecaUdeA.Business.Contracts.Platform
{
    public interface INetworkManager
    {
        bool IsConnected { get; }
    }
}
