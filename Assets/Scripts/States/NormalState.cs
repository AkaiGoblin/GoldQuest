using System;
using Assets.Scripts.Enumerations;
using UnityEngine;

namespace Assets.Scripts.States
{
	public class NormalState : PlayerState
	{
		private float _slideTime = 0f;
		private bool _isSlidding = false;
		private bool _rightDirection = false;
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
			if (_isRunning && !_isSlidding)
			{
				Slide();
				return;
			}
			_ninjaAnimator.SetBool("IsCrouching", true);
			ChangeColliderType(ColliderTypeEnum.Crouch);
			_ninjaPlayer.StateChangeDelegate(_stateFactory.CreatePlayerState(PlayerStateType.Crouching));
		}

		public override void Idle()
		{
			if (_spriteRenderer == null || _ninjaAnimator == null || _ninjaPlayer == null)
				return;
			_isRunning = false;
			_isSlidding = false;
			_slideTime = 0f;
			_ninjaAnimator.SetBool("IsRunning", false);
			_ninjaAnimator.SetBool("IsWalking", false);
			_ninjaAnimator.SetBool("IsJumping", false);
			_ninjaAnimator.SetBool("IsCrouching", false);
		}

		public override void Jump()
		{
			if (_spriteRenderer == null || _ninjaAnimator == null || _ninjaPlayer == null)
				return;
			//TODO => improve jumping mechanic
			//_rigidBody2D.velocity = Vector2.up * _jumpSpeed;
			_rigidBody2D.AddForce(Vector2.up * _ninjaPlayer.JumpSpeed, ForceMode2D.Impulse);
			_ninjaAnimator.SetBool("IsJumping", true);
			OnPlayerStateChanged(_stateFactory.CreatePlayerState(PlayerStateType.Jumping));			
		}

		public override void MoveLeft()
		{
			if (_spriteRenderer == null || _ninjaAnimator == null || _ninjaPlayer == null)
				return;
			_isRunning = true;
			_rightDirection = false;
			_spriteRenderer.flipX = true; //TODO: change the minus local scale X
			_ninjaPlayer.SpeedModifier = 1f;
			_ninjaPlayer.transform.position = _ninjaPlayer.transform.position + Move(Vector3.left);
			_ninjaAnimator.SetBool("IsRunning", true);
		}

		public override void MoveRight()
		{
			if(_spriteRenderer == null || _ninjaAnimator == null || _ninjaPlayer == null)
				return;
			_isRunning = true;
			_rightDirection = true;
			_spriteRenderer.flipX = false; //TODO: change the minus local scale X
			_ninjaPlayer.SpeedModifier = 1f;
			_ninjaPlayer.transform.position = _ninjaPlayer.transform.position + Move(Vector3.right);
			_ninjaAnimator.SetBool("IsRunning", true);
		}

		protected override void Slide()
		{
			if (_spriteRenderer == null || _ninjaAnimator == null || _ninjaPlayer == null)
				return;

			while (_slideTime <= _ninjaPlayer.SlideTimer)
			{
				_ninjaAnimator.SetBool("IsCrouching", true);				
				_isSlidding = true;
				_rigidBody2D.velocity = _rightDirection ? Vector2.right * _ninjaPlayer.Speed : Vector2.left * _ninjaPlayer.Speed;
				_slideTime += Time.deltaTime;
				
			}
				Crouch();

		}
		#endregion



	}
}
