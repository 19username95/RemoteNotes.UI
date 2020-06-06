namespace RemoteNotes.Service.Storage
{
    public interface IStorageService
    {
        T Load<T>(string key);

        void Save<T>(string key, T value);
    }
}