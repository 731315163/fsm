using System;
using InstantNoodle.FSM;

namespace FsmTest
{
    public class TestState : StateBase<string, string> ,IStart,IOver
    {
        public int starttimes = 0;
        public int overtimes = 0;
        public TestState()
        {

        }
        public TestState(string id):base(id)
        {

        }
        public void Over()
        {
            overtimes += 1;
        }

        public void Start()
        {
            starttimes += 1;
        }
    }
}
