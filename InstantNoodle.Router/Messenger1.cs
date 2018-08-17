using System;

namespace InstantNoodle.Router
{
    public partial class Messenger<TEventKey>
    {
        public void AddListener<T>(TEventKey eventType, Action<T> handler)
        {
            OnAddListener(eventType, handler);
        }

        public void AddListener<T, TReturn>(TEventKey eventType, Func<T, TReturn> handler)
        {
            OnAddListener(eventType, handler);
        }

        public void RemoveListener<T>(TEventKey eventType, Action<T> handler)
        {
            OnRemoveListener(eventType, handler);
        }

        public void RemoveListener<T, TReturn>(TEventKey eventType, Func<T, TReturn> handler)
        {
            OnRemoveListener(eventType, handler);
        }

        public void Broadcast<T>(TEventKey eventType, T arg1)
        {
            Broadcast(eventType, arg1, Mode);
        }

        public void Broadcast<T, TReturn>(TEventKey eventType, T arg1, Action<TReturn> returnCall)
        {
            Broadcast(eventType, arg1, returnCall, Mode);
        }

        public void Broadcast<T>(TEventKey eventType, T arg1, MessengerMode mode)
        {
            OnBroadcasting(eventType, mode);
            var invocationList = GetInvocationList<Action<T>>(eventType);

            for (var i = 0; i < invocationList.Count; i++)
            {
                var callback = invocationList[i];
                callback.Invoke(arg1);
            }
        }

        public void Broadcast<T, TReturn>(TEventKey eventType, T arg1, Action<TReturn> returnCall, MessengerMode mode)
        {
            OnBroadcasting(eventType, mode);
            var invocationList = GetInvocationList<Func<T, TReturn>>(eventType);

            for (var i = 0; i < invocationList.Count; i++)
            {
                var del = invocationList[i];
                var result = del.Invoke(arg1);
                returnCall.Invoke(result);
            }
        }
    }
}