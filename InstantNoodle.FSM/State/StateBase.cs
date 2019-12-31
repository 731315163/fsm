// Copyright (c) zt. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using InstantNoodle.Router;

namespace InstantNoodle.FSM
{
    public class StateBase <TStateID,TEventKey>:Messenger<TEventKey>
	{
		public TStateID Name
		{
			get;
			set;
		}

		public TStateID FatherName
		{
			get;
			set;
		}
		public StateBase() : this(default)
		{

		}
        public StateBase(TStateID id, MessengerMode mode = MessengerMode.DontRequireListener) : base(new Dictionary<TEventKey, Delegate>(),mode)
        {
			this.Name = id;
        }
	}
}