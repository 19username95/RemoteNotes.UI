using System;
using Newtonsoft.Json;
using Plugin.Settings.Abstractions;

namespace RemoteNotes.UI.Service.Storage
{
    public class StorageService : IStorageService
    {
        private readonly ISettings _settings;

        public StorageService(
            ISettings settings)
        {
            _settings = settings;
        }
        
        #region -- IStorageService implementation --
        
        public T Load<T>(string key)
        {
            var result = default(T);
            
            try
            {
                var value = _settings.GetValueOrDefault(key, string.Empty);

                if (!string.IsNullOrEmpty(value))
                {
                    result = JsonConvert.DeserializeObject<T>(value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return result;
        }

        public void Save<T>(string key, T value)
        {
            try
            {
                var serializedValue = JsonConvert.SerializeObject(value);

                _settings.AddOrUpdateValue(key, serializedValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        #endregion
    }
}