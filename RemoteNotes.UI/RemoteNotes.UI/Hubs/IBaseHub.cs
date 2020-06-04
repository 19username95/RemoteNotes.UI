using System;
using System.Threading.Tasks;
using RemoteNotes.UI.Model;

namespace RemoteNotes.UI.Hubs
{
    public interface IBaseHub
    {
        event Action<bool> Reconnected;

        bool IsConnected { get; }

        Task<Result> ConnectAsync();

        Task<Result> DisconnectAsync();
    }
}