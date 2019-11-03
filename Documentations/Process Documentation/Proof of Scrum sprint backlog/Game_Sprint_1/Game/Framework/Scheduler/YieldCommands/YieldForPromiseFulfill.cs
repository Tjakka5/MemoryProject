namespace Framework.Scheduling
{
	public class YieldForPromiseFulfill : YieldCommand
	{
		private VoidPromise promise = null;
		private bool done = false;

		public YieldForPromiseFulfill(VoidPromise promise)
		{
			this.promise = promise;

			promise.OnFulfilled += OnPromiseFulfill;
		}

		~YieldForPromiseFulfill()
		{
			promise.OnFulfilled -= OnPromiseFulfill;
		}
		
		private void OnPromiseFulfill()
		{
			done = true;
		}

		public override bool Check()
		{
			return done;
		}
	}
}