// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;

namespace InstantNoodle.Router
{
    public partial class Messenger<TEventKey>
    {
        public void AddListener(TEventKey eventType, Action handler)
        {
            OnAddListener(eventType, handler);
        }

        public void AddListener<TReturn>(TEventKey eventType, Func<TReturn> handler)
        {
            OnAddListener(eventType, handler);
        }

        public void RemoveListener(TEventKey eventType, Action handler)
        {
            OnRemoveListener(eventType, handler);
        }

        public void RemoveListener<TReturn>(TEventKey eventType, Func<TReturn> handler)
        {
            OnRemoveListener(eventType, handler);
        }

        public void Broadcast(TEventKey eventType)
        {
            Broadcast(eventType, Mode);
        }

        public void Broadcast<TReturn>(TEventKey eventType, Action<TReturn> returnCall)
        {
            Broadcast(eventType, returnCall, Mode);
        }

        public void Broadcast(TEventKey eventType, MessengerMode mode)
        {
            OnBroadcasting(eventType, mode);
            var invocationList = GetInvocationList<Action>(eventType);

            for (var index = 0; index < invocationList.Count; index++)
            {
                var callback = invocationList[index];
                callback.Invoke();
            }
        }

        public void Broadcast<TReturn>(TEventKey eventType, Action<TReturn> returnCall, MessengerMode mode)
        {
            OnBroadcasting(eventType, mode);
            IList<Func<TReturn>> invocationList = GetInvocationList<Func<TReturn>>(eventType);

            for (var i = 0; i < invocationList.Count; i++)
            {
                var del = invocationList[i];
                var result = del.Invoke();
                returnCall.Invoke(result);
            }
        }

    }
}