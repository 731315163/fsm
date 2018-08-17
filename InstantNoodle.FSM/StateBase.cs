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

using System;
using System.Collections.Generic;
using InstantNoodle.Router;

namespace InstantNoodle.FSM
{
    public class StateBase <TStateName,TEventKey>:Messenger<TEventKey>{
		
		private TStateName name = default(TStateName);
		protected TStateName fatherName = default(TStateName);
		protected TStateName defSubState = default(TStateName);

		public TStateName Name{
			get{
				return name;
			}
			set{
				name = value;
			}
		}
		
		public TStateName DefSubState{
			get{
				return defSubState;
			}
			set{
				defSubState = value;
			}
		}

		public TStateName FatherName{
			get{
				return fatherName;
			}
			set{
				fatherName = value;
			}
		}

        public StateBase(MessengerMode mode = MessengerMode.DontRequireListener) : base(new Dictionary<TEventKey, Delegate>(),mode)
        {
            
        }
	}
}