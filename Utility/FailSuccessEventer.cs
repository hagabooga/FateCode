using System;

namespace Utility
{
    public abstract class FailSuccessEventer : EzNode
    {
        public event Handlers.Message failed;
        public event Action success;

        public virtual void InvokeSuccess() => success?.Invoke();
        public virtual void InvokeFailed(string msg) => failed?.Invoke(msg);
    }
}