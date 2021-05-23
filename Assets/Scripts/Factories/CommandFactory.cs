using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Commands;
using System;

namespace Assets.Scripts.Factories
{
	public class CommandFactory
	{
		#region Fields
		private Ninja _ninjaPlayer;
		#endregion
		public CommandFactory(Ninja ninjaPlayer)
		{
			_ninjaPlayer = ninjaPlayer;
		}
		public Command GetCommand(float horizontalInput, float verticalInput)
		{
			return CreateCommand(horizontalInput, verticalInput);
		}

		private Command CreateCommand(float horizontalInput, float verticalInput)
		{
			Command nextMove = new IdleCommand(_ninjaPlayer);
			nextMove = ProcessHorizontalInput(horizontalInput) ?? nextMove;


			return nextMove;
		}

		#nullable enable
		private Command? ProcessHorizontalInput(float horizontalInput)
		{
			if (Mathf.Abs(horizontalInput) < Mathf.Epsilon) return null;
			
			if (horizontalInput > Mathf.Epsilon)
				return new MoveRightCommand(_ninjaPlayer);

			return new MoveLeftCommand(_ninjaPlayer);
		}
	}
}
