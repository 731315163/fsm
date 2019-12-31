// Copyright (c) zt. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using InstantNoodle.Router;
using System;

namespace InstantNoodle.FSM
{
    public partial class FiniteStateMachine<TStateID, TEventKey>
    {
        public void Broadcast(TEventKey eventType)
        {
            for (int i = 0; i < curstates.Count; i++)
            {
                curstates[i].Broadcast(eventType);
            }
        }

        public void Broadcast<TReturn>(TEventKey eventType, Action<TReturn> returnCall)
        {
            for (int i = 0; i < curstates.Count; i++)
            {
                curstates[i].Broadcast(eventType,returnCall);
            }
        }

        public void Broadcast(TEventKey eventType, MessengerMode mode)
        {
            for (int i = 0; i < curstates.Count; i++)
            {
                curstates[i].Broadcast(eventType,mode);
            }
        }

        public void Broadcast<TReturn>(TEventKey eventType, Action<TReturn> returnCall, MessengerMode mode)
        {
            for (int i = 0; i < curstates.Count; i++)
            {
                curstates[i].Broadcast(eventType,returnCall,mode);
            }
        }
    }
}