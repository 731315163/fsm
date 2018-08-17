using System;
using InstantNoodle.Router;

namespace InstantNoodle.FSM
{
    public partial class FiniteStateMachine<TStateName,TEventKey>
    {
        public void Broadcast<T, U, V>(TEventKey eventType, T arg1, U arg2, V arg3)
        {
            for (var i = 0; i < m_currState.Count; i++)
            {
                m_currState[i].Broadcast(eventType,arg1,arg2,arg3);
            }
        }

        public void Broadcast<T, U, V, TReturn>(TEventKey eventType, T arg1, U arg2, V arg3, Action<TReturn> returnCall)
        {
            for (var i = 0; i < m_currState.Count; i++)
            {
                m_currState[i].Broadcast(eventType, arg1, arg2, arg3,returnCall);
            }
        }

        public void Broadcast<T, U, V>(TEventKey eventType, T arg1, U arg2, V arg3, MessengerMode mode)
        {
            for (var i = 0; i < m_currState.Count; i++)
            {
                m_currState[i].Broadcast(eventType, arg1, arg2, arg3,mode);
            }
        }

        public void Broadcast<T, U, V, TReturn>(TEventKey eventType, T arg1, U arg2, V arg3, Action<TReturn> returnCall, MessengerMode mode)
        {
            for (var i = 0; i < m_currState.Count; i++)
            {
                m_currState[i].Broadcast(eventType, arg1, arg2, arg3,returnCall,mode);
            }
        }
    }
}
