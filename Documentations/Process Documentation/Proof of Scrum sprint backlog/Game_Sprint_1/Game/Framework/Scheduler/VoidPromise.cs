namespace Framework.Scheduling
{
	public class VoidPromise
	{
		public delegate void OnFinalizedHandler();
		public event OnFinalizedHandler OnFinalized;

		public delegate void OnFulfilledHandler();
		public event OnFulfilledHandler OnFulfilled;

		public delegate void OnRejectedHandler();
		public event OnRejectedHandler OnRejected;

		public enum States
		{
			Pending,
			Fulfilled,
			Rejected,
		}

		public States State
		{
			get;
			private set;
		} = States.Pending;

		public void Fulfill()
		{
			if (IsFinalized())
				throw new System.InvalidOperationException("Promise was already fulfilled");
	
			State = States.Fulfilled;

			OnFulfilled?.Invoke();
			OnFinalized?.Invoke();
		}

		public void Reject()
		{
			if (IsFinalized())
				throw new System.InvalidOperationException("Promise was already fulfilled");

			State = States.Rejected;

			OnRejected?.Invoke();
			OnFinalized?.Invoke();
		}

		public bool IsFinalized()
		{
			return State != States.Pending;
		}

		public bool IsFulFilled()
		{
			return State == States.Fulfilled;
		}
	}
}
