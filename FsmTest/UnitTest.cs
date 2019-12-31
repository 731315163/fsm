using NUnit.Framework;
using InstantNoodle.FSM;

namespace FsmTest
{
    public class Tests
    {
        private FiniteStateMachine<string, string> fsm;
        [SetUp]
        public void Setup()
        {
            fsm = new FiniteStateMachine<string, string>();
        }
        [Test]
        public void AddStateException()
        {
            var state = new StateBase<string, string>();
            //Do not use default as state ID
            Assert.Catch<StateNameIsDefault>(() => { fsm.AddState(null, state); });
        }

        [Test]
        public void Translation()
        {
            var state = new TestState("");
            fsm.AddState(state);
            fsm.Translation("");
            Assert.AreEqual(state.starttimes, 1);
        }
        [Test]    
        public void Translation1()
        {
            var state = new TestState("");
            var childstate1 = new TestState("1");
            childstate1.FatherName = state.Name;
            fsm.AddState(state);
            fsm.AddState(childstate1);
            fsm.Translation("1");
            
            Assert.AreEqual(state.starttimes, 1);
            Assert.AreEqual(childstate1.starttimes, 1);
        }
        [Test]
        public void Translation2()
        {
            Assert.Pass();
        }
        [Test]
        public void Translation3()
        {
            Assert.Pass();
        }
        [Test]
        public void GetCurState()
        {
            var state = fsm.GetCurState();
            Assert.AreEqual(state, null);

            state = new TestState("");
            fsm.AddState(state);
            fsm.Translation("");
            Assert.AreEqual(state, fsm.GetCurState());
            Assert.AreEqual(state, fsm.GetCurState(""));
        }
        [Test]
        public void ShutDownTest()
        {
            var state = new TestState("");
            fsm.AddState(state);
            fsm.ShutDown();
            fsm.Translation("");
            fsm.ShutDown();
            Assert.AreEqual(state.overtimes, 1);
        }
    }
}