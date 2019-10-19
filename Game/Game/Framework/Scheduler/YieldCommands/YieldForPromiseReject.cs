namespace Framework.Scheduling
{
	public class YieldForPromiseReject : YieldCommand
	{
		private VoidPromise promise = null;
		private bool done = false;

		public YieldForPromiseReject(VoidPromise promise)
		{
			this.promise = promise;

			promise.OnRejected += OnPromiseRejected;
		}

		~YieldForPromiseReject()
		{
			promise.OnRejected -= OnPromiseRejected;
		}

		private void OnPromiseRejected()
		{
			done = true;
		}

		public override bool Check()
		{
			return done;
		}
	}
}