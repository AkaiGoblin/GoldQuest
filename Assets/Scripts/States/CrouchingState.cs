using Assets.Scripts.Enumerations;
using UnityEngine;

namespace Assets.Scripts.States
{
	public class CrouchingState : PlayerState
	{
		public CrouchingState(Ninja ninjaPlayer,
			Animator ninjaAnimator,
			SpriteRenderer spriteRenderer,
			Rigidbody2D rigidBody2D,
			Collider2D collider2D) : base(ninjaPlayer, ninjaAnimator, spriteRenderer, rigidBody2D, collider2D) 
		{}
		public override void Crouch()
		{
			
		}

		public override void Idle()
		{
			
		}

		public override void Jump()
		{
			
		}

		public override void MoveLeft()
		{
			
		}

		public override void MoveRight()
		{
			
		}

		protected override void Slide()
		{
			
		}
	}
}
