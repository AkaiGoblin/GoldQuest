using System;
using Assets.Scripts.Enumerations;
using UnityEngine;

namespace Assets.Scripts.States
{
	public class NormalState : PlayerState
	{
		
		public NormalState(
			Ninja ninjaPlayer, 
			Animator ninjaAnimator, 
			SpriteRenderer spriteRenderer,
			Rigidbody2D rigidBody2D,
			Collider2D collider2D) : base(ninjaPlayer, ninjaAnimator, spriteRenderer, rigidBody2D, collider2D) 
		{}

		#region Common Methods
		public override void Crouch()
		{
			if (_isRunning)
			{
				Slide();
				return;
			}
			_ninjaAnimator.SetBool("IsCrouching", true);
			ChangeColliderType(ColliderTypeEnum.Crouch);
			_ninjaPlayer.StateChangeDelegate(new CrouchingState(
				_ninjaPlayer,
				_ninjaAnimator,
				_spriteRenderer,
				_rigidBody2D,
				_collider2D));
		}

		public override void Idle()
		{
			_isRunning = false;
			_ninjaAnimator.SetBool("IsRunning", false);
			_ninjaAnimator.SetBool("IsWalking", false);
			_ninjaAnimator.SetBool("IsJumping", false);
		}

		public override void Jump()
		{
			//TODO => improve jumping mechanic
			//_rigidBody2D.velocity = Vector2.up * _jumpSpeed;
			_rigidBody2D.AddForce(Vector2.up * _ninjaPlayer.JumpSpeed, ForceMode2D.Impulse);
			_ninjaAnimator.SetBool("IsJumping", true);
			OnPlayerStateChanged(new JumpingState(_ninjaPlayer, _ninjaAnimator, _spriteRenderer, _rigidBody2D, _collider2D));
		}

		public override void MoveLeft()
		{
			_isRunning = true;
			_spriteRenderer.flipX = true; //TODO: change the minus local scale X
			_ninjaPlayer.SpeedModifier = 1f;
			_ninjaPlayer.transform.position = _ninjaPlayer.transform.position + Move(Vector3.left);
			_ninjaAnimator.SetBool("IsRunning", true);
		}

		public override void MoveRight()
		{
			_isRunning = true;
			_spriteRenderer.flipX = false; //TODO: change the minus local scale X
			_ninjaPlayer.SpeedModifier = 1f;
			_ninjaPlayer.transform.position = _ninjaPlayer.transform.position + Move(Vector3.right);
			_ninjaAnimator.SetBool("IsRunning", true);
		}

		protected override void Slide()
		{
			Debug.Log("Sliding");
		}
		#endregion



	}
}
