using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Scheduler
{
	public class Promise<T>
	{
		public enum States
		{
			Pending,
			Finished,
			Failed,
		}

		public T Value
		{
			get;
			private set;
		} = default(T);

		public States State
		{
			get;
			private set;
		} = States.Pending;
		
		public Promise()
		{

		}

		public void Finalize(T value)
		{
			Value = value;
			State = States.Finished;
		}

		public void Fail()
		{
			State = States.Failed;
		}
	}
}
