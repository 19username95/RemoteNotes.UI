using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using RemoteNotes.UI.Enums;
using RemoteNotes.UI.Model;

namespace RemoteNotes.UI.Hubs
{
    public abstract class BaseHub : IBaseHub
    {
        #region -- Abstract members --

        protected abstract string HubUrl { get; }

        protected abstract void InitHubSubscriptions();

        #endregion
        
        #region -- Protected helpers --

        protected HubConnection Hub { get; set; }
        
        protected async Task CheckHubStateAsync()
        {
            await InitIfNotInitializedAsync();
            await UpdateConnectionAsync();
        }

        protected virtual void SubscribeReconnectEvents()
        {
            Hub.Closed += OnHubOnClosedAsync;
            Hub.Reconnected += OnHubOnReconnectedAsync;
        }

        protected virtual void UnsubscribeReconnectEvents()
        {
            Hub.Closed -= OnHubOnClosedAsync;
            Hub.Reconnected -= OnHubOnReconnectedAsync;
        }

        protected async Task<Result> ExecuteAsync(string methodName, params object[] parameters)
        {
            await CheckHubStateAsync();

            var result = new Result();
            
            try
            {
                var operationStatusInfo = await Hub.InvokeAsync<ServerResult>(
                    methodName,
                    parameters);

                if (operationStatusInfo.OperationStatus == EOperationStatus.Done)
                {
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure(operationStatusInfo.AttachedInfo);
                }
            }
            catch (Exception ex)
            {
                result.SetFailure(ex);
                Debug.WriteLine(ex);
            }

            return result;
        }
        
        protected async Task<Result<T>> ExecuteAndReturnAsync<T>(string methodName, params object[] parameters)
        {
            await CheckHubStateAsync();
            
            var result = new Result<T>();

            try
            {
                var operationStatusInfo = await Hub.InvokeCoreAsync<ServerResult>(
                    methodName,
                    parameters);

                if (operationStatusInfo.OperationStatus == EOperationStatus.Done)
                {
                    var attachedObjectText = operationStatusInfo.AttachedObject.ToString();
                    var convertedData = JsonConvert.DeserializeObject<T>(attachedObjectText);
                    
                    result.SetSuccess(convertedData);
                }
                else
                {
                    result.SetFailure(operationStatusInfo.AttachedInfo);
                }
            }
            catch (Exception ex)
            {
                result.SetFailure(ex);
                Debug.WriteLine(ex);
            }

            return result;
        }
        
        #endregion
        
        #region -- IBaseHub implementation --
        
        public event Action<bool> Reconnected = delegate { };

        public bool IsConnected => Hub?.State == HubConnectionState.Connected;
        
        public async Task<Result> ConnectAsync()
        {
            var result = new Result();
            Debug.WriteLine("Try connect");

            try
            {
                await Hub.StartAsync();

                if (IsConnected)
                {
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(nameof(ConnectAsync), ex.Message, ex);
                result.SetFailure(ex);
            }
            finally
            {
                RaiseHubReconnect();
            }

            return result;
        }

        public async Task<Result> DisconnectAsync()
        {
            var result = new Result();
            Debug.WriteLine("Try disconnect");

            try
            {
                await Hub.StopAsync();
                
                result.SetSuccess();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(nameof(DisconnectAsync), ex.Message, ex);
                result.SetFailure(ex);
            }
            finally
            {
                RaiseHubReconnect();
            }

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

        private async Task InitIfNotInitializedAsync()
        {
            if (Hub == null)
            {
                await InitAsync();
            }
        }
        
        private async Task InitAsync()
        {
            if (Hub != null)
            {
                UnsubscribeReconnectEvents();

                await TryDisposeAsync();
            }

            Hub = new HubConnectionBuilder()
                .WithUrl(HubUrl)
                .Build();

            SubscribeReconnectEvents();
            InitHubSubscriptions();
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
        
        private async Task TryDisposeAsync()
        {
            try
            {
                await Hub.DisposeAsync();
                Hub = null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        #endregion
    }
}