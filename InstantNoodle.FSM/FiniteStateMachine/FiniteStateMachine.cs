/*-----------------------------------------------------------------------------
The MIT License (MIT)

This source file is part of GDGeek
    (Game Develop & Game Engine Extendable Kits)
For the latest info, see http://gdgeek.com/

Copyright (c) 2014-2017 GDGeek Software Ltd

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/


using System.Collections.Generic;

namespace InstantNoodle.FSM
{
    public partial class FiniteStateMachine<TStateName,TEventKey>
    {

        private IDictionary<TStateName, StateBase<TStateName,TEventKey>> m_states = new Dictionary<TStateName, StateBase<TStateName, TEventKey>>();
        private IList<StateBase<TStateName,TEventKey>> m_currState = new List<StateBase<TStateName, TEventKey>>();
        private TStateName m_name = default(TStateName);

        public TStateName Name
        {
            get { return m_name; }
            protected set { m_name = value; }
        }
    


        public FiniteStateMachine(TStateName name)
        {
            StateBase<TStateName, TEventKey> root = new StateBase<TStateName, TEventKey> {Name = name};
            this.m_states[name] = root;
            this.m_currState.Add(root);
        }

        public void addState(StateBase<TStateName,TEventKey> state)
        {
            this.addState(state.Name, (StateBase<TStateName,TEventKey>)(state));
        }

        public void addState(TStateName stateName, StateBase<TStateName,TEventKey> state)
        {

            this.addState(stateName, state, default(TStateName));
        }


        public void addState(StateBase<TStateName,TEventKey> state, TStateName fatherName)
        {

            this.addState(state.Name, (StateBase<TStateName,TEventKey>)(state), fatherName);
        }
        /*
		public void addState(string defSubState, State state){
			this.addState (state.stateName, (StateBase)(state), "");		
			state.defSubState = defSubState;
		}
		public void addState(string stateName, string defSubState, StateBase state){
			this.addState (stateName, state, "");		
			state.defSubState = defSubState;
		}
		public void addState(string stateName, string defSubState, StateBase state, string fatherName){
			this.addState (stateName, state, fatherName);		
			state.defSubState = defSubState;
		}
*/
        public void addState(TStateName stateName, StateBase<TStateName,TEventKey> state, TStateName fatherName)
        {

            if (fatherName.Equals(default(TStateName)))
            {
                state.FatherName = Name;
            }
            else
            {
                state.FatherName = fatherName;
            }
            if (this.m_states.ContainsKey(state.FatherName))
            {
                if (this.m_states[state.FatherName].DefSubState.Equals(default(TStateName)))
                {
                    this.m_states[state.FatherName].DefSubState = stateName;
                }
            }
            state.Name = stateName;
            this.m_states[stateName] = state;
        }

        public StateBase<TStateName,TEventKey> getCurrSubState()
        {
            var self = this;
            return self.m_currState[self.m_currState.Count - 1];

        }

        public StateBase<TStateName,TEventKey> getCurrState(TStateName name)
        {
            var self = this;
            for (var i = 0; i < self.m_currState.Count; ++i)
            {
                StateBase<TStateName,TEventKey> state = self.m_currState[i];
                if (state.Name.Equals(name))
                    return state;
            }
            return null;
        }

        public void Shutdown()
        {
            for (int i = this.m_currState.Count - 1; i >= 0; --i)
            {
                StateBase<TStateName,TEventKey> state = this.m_currState[i];
                var over = state as IOver;
                if(over!= null)over.Over();
            }
            this.m_currState = null;
        }
        public void Translation(TStateName name)
        {
            if (!this.m_states.ContainsKey(name))//if no target return!
                return;

            StateBase<TStateName,TEventKey> target = this.m_states[name];//target state
            while (!target.DefSubState.Equals(default(TStateName)) && this.m_states.ContainsKey(target.DefSubState))
            {
                target = this.m_states[target.DefSubState];
            }

            //if current, reset
            if (target == this.m_currState[this.m_currState.Count - 1])
            {
                var over = target as IOver;
                if(over != null)over.Over();
                var start = target as IStart;
                if(start != null)start.Start();
                return;
            }



            StateBase<TStateName,TEventKey> publicState = null;
            IList<StateBase<TStateName,TEventKey>> stateList = new List<StateBase<TStateName, TEventKey>>();

            StateBase<TStateName,TEventKey> tempState = target;
            TStateName fatherName = tempState.FatherName;

            //do loop 
            while (tempState != null)
            {
                //reiterator current list
                for (var i = this.m_currState.Count - 1; i >= 0; i--)
                {
                    StateBase<TStateName,TEventKey> state = this.m_currState[i] as StateBase<TStateName,TEventKey>;
                    //if has public 
                    if (state == tempState)
                    {
                        publicState = state;
                        break;
                    }
                }

                //end
                if (publicState != null)
                    break;

                //else push state_list
                stateList.Insert(0, tempState);
                //state_list.unshift(temp_state);

                if (fatherName.Equals(default(TStateName)))
                {
                    tempState = this.m_states[fatherName];
                    fatherName = tempState.FatherName;
                }
                else
                {
                    tempState = null;
                }

            }
            //if no public return
            if (publicState == null)
                return;
            IList<StateBase<TStateName,TEventKey>> newCurrState = new List<StateBase<TStateName, TEventKey>>() ;
            bool under = true;
            //-- 析构状态
            for (int i2 = this.m_currState.Count - 1; i2 >= 0; --i2)
            {
                StateBase<TStateName,TEventKey> state2 = this.m_currState[i2];
                if (state2 == publicState)
                {
                    under = false;
                }
                if (under)
                {
                    var state = state2 as IOver;
                    state.Over();
                }
                else
                {
                    newCurrState.Insert(0, state2);
                }
            }

            //-- 构建状态
            for (int i3 = 0; i3 < stateList.Count; ++i3)
            {
                StateBase<TStateName,TEventKey> state3 = stateList[i3];
                var state = state3 as IStart;
                if(state != null)state.Start();
                newCurrState.Add(state3);
            }

            this.m_currState = newCurrState;

        }
    
    }
}