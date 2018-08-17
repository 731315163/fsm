using System;

namespace InstantNoodle.Router
{
    public partial class Messenger<TEventKey>
    {
        public void AddListener<T, U>(TEventKey eventType, Action<T, U> handler)
        {
            OnAddListener(eventType, handler);
        }

        public void AddListener<T, U, TReturn>(TEventKey eventType, Func<T, U, TReturn> handler)
        {
            OnAddListener(eventType, handler);
        }

        public void RemoveListener<T, U>(TEventKey eventType, Action<T, U> handler)
        {
            OnRemoveListener(eventType, handler);
        }

        public void RemoveListener<T, U, TReturn>(TEventKey eventType, Func<T, U, TReturn> handler)
        {
            OnRemoveListener(eventType, handler);
        }

        public void Broadcast<T, U>(TEventKey eventType, T arg1, U arg2)
        {
            Broadcast(eventType, arg1, arg2, Mode);
        }

        public void Broadcast<T, U, TReturn>(TEventKey eventType, T arg1, U arg2, Action<TReturn> returnCall)
        {
            Broadcast(eventType, arg1, arg2, returnCall, Mode);
        }

        public void Broadcast<T, U>(TEventKey eventType, T arg1, U arg2, MessengerMode mode)
        {
            OnBroadcasting(eventType, mode);
            var invocationList = GetInvocationList<Action<T, U>>(eventType);

            for (var i = 0; i < invocationList.Count; i++)
            {
                var callback = invocationList[i];
                callback.Invoke(arg1, arg2);
            }
        }

        public void Broadcast<T, U, TReturn>(TEventKey eventType, T arg1, U arg2, Action<TReturn> returnCall, MessengerMode mode)
        {
            OnBroadcasting(eventType, mode);
            var invocationList = GetInvocationList<Func<T, U, TReturn>>(eventType);

            for (var index = 0; index < invocationList.Count; index++)
            {
                var del = invocationList[index];
                var result = del.Invoke(arg1, arg2);
                returnCall.Invoke(result);
            }
        }

    }
}