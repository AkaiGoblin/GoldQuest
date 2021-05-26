using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enumerations;
using UnityEngine;

namespace Assets.Scripts.States
{
	public abstract class PlayerState
	{
		#region Delegates
		public delegate void StoppedCrouchingHandler();
		#endregion

		protected Ninja _ninjaPlayer;
		protected Animator _ninjaAnimator;
		protected SpriteRenderer _spriteRenderer;
		protected Rigidbody2D _rigidBody2D;
		protected Collider2D _collider2D;
		protected Collider2D _crouchCollider2D;
		protected bool _isRunning = false;
		public StoppedCrouchingHandler StoppedCrouchingDelegate;

		public PlayerState(
			Ninja ninjaPlayer,
			Animator ninjaAnimator,
			SpriteRenderer spriteRenderer,
			Rigidbody2D rigidBody2D,
			Collider2D collider2D)
		{
			_ninjaPlayer = ninjaPlayer;
			_ninjaAnimator = ninjaAnimator;
			_spriteRenderer = spriteRenderer;
			_rigidBody2D = rigidBody2D;
			_collider2D = collider2D;
			_crouchCollider2D = _ninjaPlayer.gameObject.GetComponentsInChildren<Collider2D>(true).First(
				x => x.gameObject.name.Equals("CrouchCollider")
				);
			StoppedCrouchingDelegate = new StoppedCrouchingHandler(StoppedCrouching);
		}

		#region Abstract Methods
		public abstract void MoveRight();
		public abstract void MoveLeft();
		public abstract void Jump();
		public abstract void Crouch();
		protected abstract void Slide();
		public abstract void Idle();
		#endregion

		protected void OnPlayerStateChanged(PlayerState newState)
		{
			if (_ninjaPlayer.StateChangeDelegate !=null)
			{
				_ninjaPlayer.StateChangeDelegate(newState);
			}
		}

		protected Vector3 Move(Vector3 direction)
		{
			return _ninjaPlayer.Speed * _ninjaPlayer.SpeedModifier * Time.deltaTime * direction;
		}
		protected void StoppedCrouching()
		{
			if (!_isRunning)
			{
				_ninjaAnimator.SetBool("IsCrouching", false);
				ChangeColliderType(ColliderTypeEnum.Standing);
				if (!(_ninjaPlayer.CurrentState is NormalState))
				{
					_ninjaPlayer.StateChangeDelegate(new NormalState(
					_ninjaPlayer,
					_ninjaAnimator,
					_spriteRenderer,
					_rigidBody2D,
					_collider2D));
				}
				

			}			
		}

		protected void ChangeColliderType(ColliderTypeEnum type)
		{
			if (type.Equals(ColliderTypeEnum.Crouch))
			{
				_crouchCollider2D.enabled = true;				
				_collider2D.enabled = false;

			}
			if (type.Equals(ColliderTypeEnum.Standing))
			{				
				_collider2D.enabled = true;
				_crouchCollider2D.enabled = false;
			}
		}

		public override string ToString()
		{
			if (this is NormalState)
			{
				return "NormalState";
			}
			if(this is CrouchingState)
			{
				return "CrouchingState";
			}
			if (this is JumpingState)
			{
				return "JumpingState";
			}
			return "Nothing";
		}

	}
}

