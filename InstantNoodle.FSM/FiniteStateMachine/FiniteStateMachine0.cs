using InstantNoodle.Router;
using System;

namespace InstantNoodle.FSM
{
    public partial class FiniteStateMachine<TStateName, TEventKey>
    {
        public void Broadcast(TEventKey eventType)
        {
            for (int i = 0; i < m_currState.Count; i++)
            {
                m_currState[i].Broadcast(eventType);
            }
        }

        public void Broadcast<TReturn>(TEventKey eventType, Action<TReturn> returnCall)
        {
            for (int i = 0; i < m_currState.Count; i++)
            {
                m_currState[i].Broadcast(eventType,returnCall);
            }
        }

        public void Broadcast(TEventKey eventType, MessengerMode mode)
        {
            for (int i = 0; i < m_currState.Count; i++)
            {
                m_currState[i].Broadcast(eventType,mode);
            }
        }

        public void Broadcast<TReturn>(TEventKey eventType, Action<TReturn> returnCall, MessengerMode mode)
        {
            for (int i = 0; i < m_currState.Count; i++)
            {
                m_currState[i].Broadcast(eventType,returnCall,mode);
            }
        }
    }
}