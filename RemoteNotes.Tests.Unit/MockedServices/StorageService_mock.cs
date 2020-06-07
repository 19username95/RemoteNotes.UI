using System.Collections.Generic;
using RemoteNotes.Service.Storage;

namespace RemoteNotes.Tests.Unit.MockedServices
{
    public class StorageService_mock : IStorageService
    {
        private Dictionary<string, object> _savedInstances;
        
        public StorageService_mock()
        {
            _savedInstances = new Dictionary<string, object>();
        }

        public T Load<T>(string key)
        {
            return (T) _savedInstances[key];
        }

        public void Save<T>(string key, T value)
        {
            _savedInstances.Add(key, value);
        }
    }
}
