namespace Framework.Scheduling
{
	public class YieldForPromiseFinalize : YieldCommand
	{
		private VoidPromise promise = null;
		private bool done = false;

		public YieldForPromiseFinalize(VoidPromise promise)
		{
			this.promise = promise;

			promise.OnFinalized += OnPromiseFinalized;
		}

		~YieldForPromiseFinalize()
		{
			promise.OnFinalized -= OnPromiseFinalized;
		}

		private void OnPromiseFinalized()
		{
			done = true;
		}

		public override bool Check()
		{
			return done;
		}
	}
}