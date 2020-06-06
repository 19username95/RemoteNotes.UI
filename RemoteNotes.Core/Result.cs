using System;

namespace RemoteNotes.Core
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }

        public string Message { get; protected set; }

        public Exception Exception { get; protected set; }

        public void SetSuccess()
        {
            IsSuccess = true;
        }

        public void SetFailure()
        {
            IsSuccess = false;
        }

        public void SetFailure(string message)
        {
            IsSuccess = false;
            Message = message;
        }

        public void SetFailure(Exception exception)
        {
            IsSuccess = false;
            Exception = exception;
            Message = exception?.Message;
        }
    }

    public class Result<T> : Result
    {
        public T SuccessResult { get; protected set; }

        public void SetSuccess(T result)
        {
            IsSuccess = true;
            SuccessResult = result;
        }
    }
}
