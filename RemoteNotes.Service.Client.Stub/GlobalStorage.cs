using RemoteNotes.Service.Domain.Data;

namespace RemoteNotes.Service.Client.Contract
{
    internal static class GlobalStorage
    {
        internal static Member CurrentMember { get; set; }
    }
}