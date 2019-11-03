namespace Framework.Scheduling
{
	public class YieldCommand
	{
		public virtual void Update(float deltaTime) { }
		public virtual bool Check()
		{
			return true;
		}
	}
}
