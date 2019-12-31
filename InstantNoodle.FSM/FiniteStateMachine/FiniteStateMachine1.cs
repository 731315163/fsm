// Copyright (c) zt. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using InstantNoodle.Router;
using System;

namespace InstantNoodle.FSM
{
    public partial class FiniteStateMachine<TStateID, TEventKey>
    {
        public void Broadcast<T>(TEventKey eventType, T arg1)
        {
            for (int i = 0; i < curstates.Count; i++)
            {
                curstates[i].Broadcast(eventType,arg1);
            }
        }

        public void Broadcast<T, TReturn>(TEventKey eventType, T arg1, Action<TReturn> returnCall)
        {
            for (int i = 0; i < curstates.Count; i++)
            {
                curstates[i].Broadcast(eventType, arg1,returnCall);
            }
        }

        public void Broadcast<T>(TEventKey eventType, T arg1, MessengerMode mode)
        {
            for (int i = 0; i < curstates.Count; i++)
            {
                curstates[i].Broadcast(eventType, arg1,mode);
            }
        }

        public void Broadcast<T, TReturn>(TEventKey eventType, T arg1, Action<TReturn> returnCall, MessengerMode mode)
        {
            for (int i = 0; i < curstates.Count; i++)
            {
                curstates[i].Broadcast(eventType, arg1,returnCall,mode);
            }
        }
        
    }
}