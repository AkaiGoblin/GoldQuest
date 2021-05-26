using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States
{
	public class JumpingState : PlayerState
	{
		private string _direction;		
		public JumpingState(
			Ninja ninjaPlayer,
			Animator ninjaAnimator,
			SpriteRenderer spriteRenderer,
			Rigidbody2D rigidBody2D,
			Collider2D collider2D) : base(ninjaPlayer, ninjaAnimator, spriteRenderer, rigidBody2D, collider2D) { }

		#region Player State Method
		public override void Crouch()
		{ }

		public override void Idle()
		{ }

		public override void Jump()
		{
			//TODO: Add some code to add double jump
		}

		public override void MoveLeft()
		{
			//TODO: Might want to add some movement with a speed modifier			
			if (_direction == null)
			{
				_direction = "Left";				
			}
			
			SetSpeedModifier("Left");
			_spriteRenderer.flipX = true;			
			_ninjaPlayer.transform.position = _ninjaPlayer.transform.position + Move(Vector3.left);
		}

		public override void MoveRight()
		{
			if (_direction == null)
			{
				_direction = "Right";
			}

			SetSpeedModifier("Right");
			_spriteRenderer.flipX = false;			
			_ninjaPlayer.transform.position = _ninjaPlayer.transform.position + Move(Vector3.right);
		}

		protected override void Slide()
		{
			//TODO: Might want to add sliding code after a jump
		}
		#endregion



		private void SetSpeedModifier(string direction)
		{
			if (direction.Equals(_direction))
			{
				_ninjaPlayer.SpeedModifier = 1f;
			}

			if (!direction.Equals(_direction))
			{
				_ninjaPlayer.SpeedModifier = 0.5f;
			}
		}
	}
}
