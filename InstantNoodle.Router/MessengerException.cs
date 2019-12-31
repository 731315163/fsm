// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;

namespace InstantNoodle.Router
{
    public class BroadcastException : Exception
    {
        public BroadcastException(string msg)
            : base(msg)
        {
        }
    }

    public class ListenerException : Exception
    {
        public ListenerException(string msg)
            : base(msg)
        {
        }
    }
}
