using System;

namespace InstantNoodle.Router
{
    public partial class Messenger<TEventKey>
    {
        public void AddListener<T, U, V>(TEventKey eventType, Action<T, U, V> handler)
        {
            OnAddListener(eventType, handler);
        }

        public void AddListener<T, U, V, TReturn>(TEventKey eventType, Func<T, U, V, TReturn> handler)
        {
            OnAddListener(eventType, handler);
        }

        public void RemoveListener<T, U, V>(TEventKey eventType, Action<T, U, V> handler)
        {
            OnRemoveListener(eventType, handler);
        }

        public void RemoveListener<T, U, V, TReturn>(TEventKey eventType, Func<T, U, V, TReturn> handler)
        {
            OnRemoveListener(eventType, handler);
        }

        public void Broadcast<T, U, V>(TEventKey eventType, T arg1, U arg2, V arg3)
        {
            Broadcast(eventType, arg1, arg2, arg3, Mode);
        }

        public void Broadcast<T, U, V, TReturn>(TEventKey eventType, T arg1, U arg2, V arg3, Action<TReturn> returnCall)
        {
            Broadcast(eventType, arg1, arg2, arg3, returnCall, Mode);
        }

        public void Broadcast<T, U, V>(TEventKey eventType, T arg1, U arg2, V arg3, MessengerMode mode)
        {
            OnBroadcasting(eventType, mode);
            var invocationList = GetInvocationList<Action<T, U, V>>(eventType);

            for (var index = 0; index < invocationList.Count; index++)
            {
                var callback = invocationList[index];
                callback.Invoke(arg1, arg2, arg3);
            }
        }

        public void Broadcast<T, U, V, TReturn>(TEventKey eventType, T arg1, U arg2, V arg3, Action<TReturn> returnCall, MessengerMode mode)
        {
            OnBroadcasting(eventType, mode);
            var invocationList = GetInvocationList<Func<T, U, V, TReturn>>(eventType);

            for (var i = 0; i < invocationList.Count; i++)
            {
                var del = invocationList[i];
                var result = del.Invoke(arg1, arg2, arg3);
                returnCall.Invoke(result);
            }
        }

    }
}