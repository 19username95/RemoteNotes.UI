namespace RemoteNotes.Core
{
    public class ServerResult
    {
        public ServerResult(EOperationStatus operationStatus)
        {
            OperationStatus = operationStatus;
        }

        public ServerResult(EOperationStatus operationStatus, object attachedObject)
        {
            OperationStatus = operationStatus;
            AttachedObject = attachedObject;
        }

        public ServerResult(EOperationStatus operationStatus, string attachedInfo)
        {
            OperationStatus = operationStatus;
            AttachedInfo = attachedInfo;
        }

        public string AttachedInfo { get; set; } = string.Empty;

        public object AttachedObject { get; set; }

        public EOperationStatus OperationStatus { get; set; }
    }
}