using NUnit.Framework;

namespace MessengerTest
{
    using InstantNoodle.Router;
    using System;
    using System.Collections.Generic;

    [TestFixture]
	class MessengerUnitTest
	{

		private readonly string eventType1 = "__testEvent1";
		private readonly string eventType2 = "__testEvent2";

		bool wasCalled = false;
		private Messenger<string> messenger;

		[OneTimeSetUp]
		public void RunTest()
		{
			messenger = new Messenger<string>(new Dictionary<string, Delegate>());
		}

		[Test]
		public void RunAddTests()
		{
			messenger.AddListener(eventType1, TestCallback);
			Assert.Catch<ListenerException>(()=> { messenger.AddListener<float>(eventType1, TestCallbackFloat); });
			
			//监听一个事件
			
			messenger.AddListener<float>(eventType2, TestCallbackFloat);
		}

		[Test]
		public void RunBroadcastTests()
		{
			wasCalled = false;
			messenger.Broadcast(eventType1);
			Assert.IsTrue(wasCalled);
			
			wasCalled = false;
			messenger.Broadcast<float>(eventType2, 1.0f);
			Assert.IsTrue(wasCalled);

			// No listener should exist for this event, but we don't require a listener so it should pass
			Assert.Catch<BroadcastException>(() => { messenger.Broadcast<float>(eventType2 + "_", 1.0f, MessengerMode.DontRequireListener); });

				// Broadcasting for an event there exists listeners for, but using wrong signature
				Assert.Catch<BroadcastException>(() => { messenger.Broadcast<float>(eventType1, 1.0f, MessengerMode.DontRequireListener); });
			// Same thing, but now we (implicitly) require at least one listener
			Assert.Catch<BroadcastException>(() => { messenger.Broadcast<float>(eventType2 + "_", 1.0f);});




			// Wrong generic type for this broadcast, and we implicitly require a listener
			Assert.Catch<BroadcastException>(() => { messenger.Broadcast<double>(eventType2, 1.0); });
				
			

		}

		[Test]
		public void RunRemoveTests()
		{
			Assert.Catch<ListenerException>(() => { messenger.RemoveListener<float>(eventType1, TestCallbackFloat); });
			

			messenger.RemoveListener(eventType1, TestCallback);

			Assert.Catch<ListenerException>(() => { messenger.RemoveListener(eventType1, TestCallback); });



			// Repeated removal should fail

			messenger.RemoveListener<float>(eventType2, TestCallbackFloat);
			Assert.Catch<ListenerException>(() => { messenger.RemoveListener<float>(eventType2, TestCallbackFloat); });
		
		}


		void TestCallback()
		{
			wasCalled = true;
			Assert.Pass();
		}

		void TestCallbackFloat(float f)
		{
			wasCalled = true;
			Assert.AreEqual(f, 1.0f);
		}



	}
}