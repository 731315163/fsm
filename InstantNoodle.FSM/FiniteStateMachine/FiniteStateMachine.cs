// Copyright (c) zt. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;

namespace InstantNoodle.FSM
{
    public partial class FiniteStateMachine<TStateID,TEventKey>
    {

        protected IDictionary<TStateID, StateBase<TStateID,TEventKey>> states = new Dictionary<TStateID, StateBase<TStateID, TEventKey>>();
        protected IList<StateBase<TStateID,TEventKey>> curstates = new List<StateBase<TStateID, TEventKey>>();

        public TStateID Name
        {
            get;
            protected set;
        }



        //public FiniteStateMachine(TStateID rootstate) 
        //{
        //    StateBase<TStateID, TEventKey> root = new StateBase<TStateID, TEventKey> {Name = rootstate};
        //    this.states[rootstate] = root;
        //    this.curstates.Add(root);
        //}

        public void AddState(StateBase<TStateID,TEventKey> state)
        {
            this.AddState(state.Name, state,state.FatherName);
        }

        public void AddState(TStateID stateName, StateBase<TStateID,TEventKey> state)
        {
            this.AddState(stateName, state, state.FatherName);
        }

        public void AddState(StateBase<TStateID,TEventKey> state, TStateID fatherName)
        {
            this.AddState(state.Name, state, fatherName);
        }
       
        public void AddState(TStateID stateName, StateBase<TStateID,TEventKey> state, TStateID fatherName)
        {
            if (Equals(stateName, default(TStateID)))
                throw new StateNameIsDefault();
            state.FatherName = fatherName;
            state.Name = stateName;
            this.states[stateName] = state;
        }

        public StateBase<TStateID,TEventKey> GetCurState()
        {
            if (this.curstates == null || this.curstates.Count == 0)
                return null;
            return this.curstates[this.curstates.Count - 1];

        }

        public StateBase<TStateID,TEventKey> GetCurState(TStateID name)
        {
            if (this.curstates == null || this.curstates.Count == 0)
                return null;
            for (var i = 0; i < this.curstates.Count; ++i)
            {
                StateBase<TStateID,TEventKey> state = this.curstates[i];
                if (Equals(state.Name,name))
                    return state;
            }
            return null;
        }

        public void ShutDown()
        {
            if (this.states == null || this.states.Count == 0)
                return;
            for (int i = this.curstates.Count - 1; i >= 0; --i)
            {
                StateBase<TStateID,TEventKey> state = this.curstates[i];
                var over = state as IOver;
                if(over!= null)over.Over();
            }
            this.curstates = null;
        }
       
        public void Translation(TStateID id)
        {
            if (!this.states.ContainsKey(id))
                throw new StateNullException();
            var line = new List<StateBase<TStateID, TEventKey>>();
            GetStateLine(line, this.states[id]);
            int curstatescount = this.curstates == null ? 0 : this.curstates.Count;
            int length = line.Count > curstatescount ? curstatescount : line.Count;
            int publicfathter = -1;
            for(int i = 0; i < length; i++)
            {
                if (Equals(line[i].Name,this.curstates[i].Name))
                {
                    publicfathter = i;
                }
                else
                {
                    break;
                }
            }
            for(int over = curstatescount - 1; over > publicfathter; over--)
            {
                (this.curstates[over] as IOver).Over();
            }
            for(int start = publicfathter + 1;start < line.Count; start++)
            {
                (line[start] as IStart).Start();
            }
            this.curstates = line;

        }
        protected void GetStateLine(IList<StateBase<TStateID,TEventKey>> list, StateBase<TStateID, TEventKey> state)
        {
            if(!Equals(state.FatherName, default(TStateID)))
            {
                if (this.states.TryGetValue(state.FatherName, out StateBase<TStateID, TEventKey> father))
                {
                    GetStateLine(list, father);
                }
            }
            list.Add(state);
        }
   
    
    }
}