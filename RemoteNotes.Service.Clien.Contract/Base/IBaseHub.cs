using RemoteNotes.Core;
using System;
using System.Threading.Tasks;

namespace RemoteNotes.Service.Client.Contract.Base
{
    public interface IBaseHub
    {
        event Action<bool> Reconnected;

        bool IsConnected { get; }

        Task<Result> ConnectAsync();

        Task<Result> DisconnectAsync();
    }
}