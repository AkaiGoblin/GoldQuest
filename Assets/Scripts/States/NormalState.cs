﻿using System;
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
			_isSlidding = false;
			_slideTime = 0f;
			_ninjaAnimator.SetBool("IsRunning", false);
			_ninjaAnimator.SetBool("IsWalking", false);
			_ninjaAnimator.SetBool("IsJumping", false);
			_ninjaAnimator.SetBool("IsCrouching", false);
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
			_rightDirection = false;
			_spriteRenderer.flipX = true; //TODO: change the minus local scale X
			_ninjaPlayer.SpeedModifier = 1f;
			_ninjaPlayer.transform.position = _ninjaPlayer.transform.position + Move(Vector3.left);
			_ninjaAnimator.SetBool("IsRunning", true);
		}

		public override void MoveRight()
		{
			_isRunning = true;
			_rightDirection = true;
			_spriteRenderer.flipX = false; //TODO: change the minus local scale X
			_ninjaPlayer.SpeedModifier = 1f;
			_ninjaPlayer.transform.position = _ninjaPlayer.transform.position + Move(Vector3.right);
			_ninjaAnimator.SetBool("IsRunning", true);
		}

		protected override void Slide()
		{
			
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
