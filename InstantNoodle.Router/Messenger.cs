// Copyright (c) zt. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;

namespace InstantNoodle.Router
{
    public enum MessengerMode
    {
        DontRequireListener,
        RequireListener,
    }

    public partial class Messenger<TEventKey>
    {
        protected readonly IDictionary<TEventKey, Delegate> eventTable;
        public MessengerMode Mode = MessengerMode.DontRequireListener;

       
        public Messenger(IDictionary<TEventKey, Delegate> eventTable ,MessengerMode mode = MessengerMode.DontRequireListener)
        {
            this.eventTable = eventTable;
            this.Mode = mode;
        }
        protected void OnAddListener(TEventKey eventType, Delegate callback)
        {
            OnListenerAdding(eventType, callback);
            eventTable[eventType] = Delegate.Combine(eventTable[eventType], callback);
        }

        protected void OnRemoveListener(TEventKey eventType, Delegate handler)
        {
            OnListenerRemoving(eventType, handler);
            eventTable[eventType] = Delegate.Remove(eventTable[eventType], handler);
            OnListenerRemoved(eventType);
        }

        public void RemoveAllListeners()
        {
            eventTable.Clear();
        }
        protected IList<T> GetInvocationList<T>(TEventKey eventType) where T:class 
        {
            if (eventTable.TryGetValue(eventType, out Delegate d))
            {
                try
                {
                    IList<T> list = new List<T>();
                    foreach (Delegate unknown in d.GetInvocationList())
                        list.Add(unknown as T);
                    return list;
                }
                catch
                {
                    throw new BroadcastException(string.Format("Broadcasting message {0} but listeners have a different signature than the broadcaster.", eventType.ToString()));
                }
            }
            return new T[0];
        }

        protected void OnListenerAdding(TEventKey eventType, Delegate listenerBeingAdded)
        {
            if (!eventTable.ContainsKey(eventType))
            {
                eventTable.Add(eventType, null);
            }

            var d = eventTable[eventType];
            if (d != null && d.GetType() != listenerBeingAdded.GetType())
            {
                throw new ListenerException(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
            }
        }

        protected void OnListenerRemoving(TEventKey eventType, Delegate listenerBeingRemoved)
        {
            if (eventTable.ContainsKey(eventType))
            {
                var d = eventTable[eventType];

                if (d == null)
                {
                    throw new ListenerException(string.Format("Attempting to remove listener with for event type {0} but current listener is null.", eventType));
                }
                else if (d.GetType() != listenerBeingRemoved.GetType())
                {
                    throw new ListenerException(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
                }
            }
            else
            {
                throw new ListenerException(string.Format("Attempting to remove listener for type {0} but Messenger doesn't know about this event type.", eventType));
            }
        }

        protected void OnListenerRemoved(TEventKey eventType)
        {
            if (eventTable[eventType] == null)
            {
                eventTable.Remove(eventType);
            }
        }

        public void OnBroadcasting(TEventKey eventType, MessengerMode mode)
        {
            if (mode == MessengerMode.RequireListener && !eventTable.ContainsKey(eventType))
            {
                throw new BroadcastException(string.Format("Broadcasting message {0} but no listener found.", eventType));
            }
        }
    }
}