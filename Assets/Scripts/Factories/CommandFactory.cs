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

#nullable enable
		private Command? _lastCrouchMove;
		#endregion
		public CommandFactory(Ninja ninjaPlayer)
		{
			_ninjaPlayer = ninjaPlayer;
		}
		public Command GetCommand(
			float horizontalInput,
			bool jumpInput,
			bool crouchInput)
		{
			return CreateCommand(horizontalInput, jumpInput, crouchInput);
		}

		private Command CreateCommand(
			float horizontalInput,
			bool jumpInput,
			bool crouchInput)
		{
			Command nextMove = new IdleCommand(_ninjaPlayer);
			
			nextMove = ProcessHorizontalInput(horizontalInput) ?? nextMove;
			nextMove = ProcessJumpInput(jumpInput) ?? nextMove;
			nextMove = ProcessCrouchInput(crouchInput) ?? nextMove;



			return nextMove;
		}

#nullable enable
		private Command? ProcessCrouchInput(bool crouchInput)
		{
			if (_lastCrouchMove == null && crouchInput)
			{
				_lastCrouchMove = new CrouchCommand(_ninjaPlayer);
				return _lastCrouchMove;
			}
			if (_lastCrouchMove != null && !crouchInput)
			{
				_lastCrouchMove = null;
				return _lastCrouchMove;
			}
			return null;
		}

#nullable enable
		private Command? ProcessJumpInput(bool jumpInput)
		{
			if (jumpInput)
			{
				return new JumpCommand(_ninjaPlayer);
			}
			return null;
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
