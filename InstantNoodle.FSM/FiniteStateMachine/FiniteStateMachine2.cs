using System;
using InstantNoodle.Router;

namespace InstantNoodle.FSM
{
    public partial class FiniteStateMachine<TStateName,TEventKey>
    {
        public void Broadcast<T, U>(TEventKey eventType, T arg1, U arg2)
        {
            for (var i = 0; i < m_currState.Count; i++)
            {
                m_currState[i].Broadcast(eventType,arg1,arg2);
            }
        }

        public void Broadcast<T, U, TReturn>(TEventKey eventType, T arg1, U arg2, Action<TReturn> returnCall)
        {
            for (var i = 0; i < m_currState.Count; i++)
            {
                m_currState[i].Broadcast(eventType, arg1, arg2,returnCall);
            }
        }

        public void Broadcast<T, U>(TEventKey eventType, T arg1, U arg2, MessengerMode mode)
        {
            for (var i = 0; i < m_currState.Count; i++)
            {
                m_currState[i].Broadcast(eventType, arg1, arg2,mode);
            }
        }

        public void Broadcast<T, U, TReturn>(TEventKey eventType, T arg1, U arg2, Action<TReturn> returnCall, MessengerMode mode)
        {
            for (var i = 0; i < m_currState.Count; i++)
            {
                m_currState[i].Broadcast(eventType, arg1, arg2,returnCall,mode);
            }
        }

    }
}
