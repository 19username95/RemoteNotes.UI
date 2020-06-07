using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract.Base;

namespace RemoteNotes.Service.Client.Contract.Hubs
{
    public abstract class BaseHub : IBaseHub
    {
        #region -- Abstract members --

        protected abstract string HubUrl { get; }

        protected abstract void InitHubSubscriptions();

        #endregion
        
        #region -- Protected helpers --

        protected async Task CheckHubStateAsync()
        {
            await InitIfNotInitializedAsync();
            await UpdateConnectionAsync();
        }

        protected virtual void SubscribeReconnectEvents()
        {
        }

        protected virtual void UnsubscribeReconnectEvents()
        {
        }

        protected async Task<Result> ExecuteAsync(string methodName, params object[] parameters)
        {
            var result = new Result();
            result.SetSuccess();
            return result;
        }
        
        protected async Task<Result<T>> ExecuteAndReturnAsync<T>(string methodName, params object[] parameters)
        {
            await CheckHubStateAsync();
            
            var result = new Result<T>();
            result.SetSuccess();
            return result;
        }
        
        #endregion
        
        #region -- IBaseHub implementation --
        
        public event Action<bool> Reconnected = delegate { };

        public bool IsConnected => true;
        
        public async Task<Result> ConnectAsync()
        {
            var result = new Result();
            Debug.WriteLine("Try connect");
            
            if (IsConnected)
            {
                result.SetSuccess();
            }
            else
            {
                result.SetFailure();
            }

            return result;
        }

        public async Task<Result> DisconnectAsync()
        {
            var result = new Result();
            Debug.WriteLine("Try disconnect");
            result.SetSuccess();
            return result;
        }
        
        #endregion

        #region -- Hub events --
        
        private async Task OnHubOnReconnectedAsync(string arg)
        {
            Debug.WriteLine(arg);
            RaiseHubReconnect();
        }

        private async Task OnHubOnClosedAsync(Exception arg)
        {
            Debug.WriteLine(arg);
            RaiseHubReconnect();
        }
        
        #endregion
        
        #region -- Private methods --

        private Task InitIfNotInitializedAsync()
        {
            return Task.FromResult(1);
        }
        
        private Task InitAsync()
        {
            SubscribeReconnectEvents();
            InitHubSubscriptions();
            return Task.FromResult(1);
        }

        private async Task UpdateConnectionAsync()
        {
            if (!IsConnected)
            {
                await ConnectAsync();
            }
        }

        private void RaiseHubReconnect()
        {
            Reconnected.Invoke(IsConnected);
            Debug.WriteLine("RaiseHubReconnect raised! Hub on url " + HubUrl + " " +
                            "reconnected with status IsConnected = " + IsConnected);
        }
        
        private Task TryDisposeAsync()
        {
            return Task.FromResult(1);
        }

        #endregion
    }
}