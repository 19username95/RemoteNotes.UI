using RemoteNotes.UI.Model;

namespace RemoteNotes.UI.Service.Storage
{
    public interface IStorageService
    {
        T Load<T>(string key);

        void Save<T>(string key, T value);
    }
}