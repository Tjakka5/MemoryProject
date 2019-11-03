namespace Framework.Scheduling
{
	public class Promise<T> : VoidPromise
	{
		public T Value
		{
			get;
			private set;
		} = default(T);

		public void Fulfill(T value)
		{
			Value = value;
			base.Fulfill();
		}
	}
}
