using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Commands
{
	public abstract class Command
	{
		private bool _isExecuted = false;
		protected Ninja _ninjaPlayer;

		public Command(Ninja ninjaPlayer)
		{
			_ninjaPlayer = ninjaPlayer;
		}

		public void Execute()
		{
			_isExecuted = MoveInDirection();
		}

		public bool HasExecuted()
		{
			return _isExecuted;
		}

		protected abstract bool MoveInDirection();
	}
}

