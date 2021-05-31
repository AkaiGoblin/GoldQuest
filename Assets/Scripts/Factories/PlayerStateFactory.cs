using Assets.Scripts.States;
using System;
using UnityEngine;

namespace Assets.Scripts.Factories
{
	public sealed class PlayerStateFactory
	{
		private Ninja _ninjaPlayer;
		private Animator _animator;
		private SpriteRenderer _spriteRenderer;
		private Rigidbody2D _rigidBody2D;
		private CapsuleCollider2D _collider2D;

		private static readonly Lazy<PlayerStateFactory> _playerStateFactory = new Lazy<PlayerStateFactory>(() => new PlayerStateFactory());

		public static PlayerStateFactory GetInstance()
		{
			return _playerStateFactory.Value;
		}

		private PlayerStateFactory()
		{
			_ninjaPlayer = GameObject.FindObjectOfType<Ninja>();
			_animator = _ninjaPlayer.gameObject.GetComponent<Animator>();
			_spriteRenderer = _ninjaPlayer.gameObject.GetComponent<SpriteRenderer>();
			_rigidBody2D = _ninjaPlayer.gameObject.GetComponent<Rigidbody2D>();
			_collider2D = _ninjaPlayer.gameObject.GetComponent<CapsuleCollider2D>();
		}

		public PlayerState CreatePlayerState(PlayerStateType newState)
		{
			PlayerState createdState;
			switch (newState)
			{
				case PlayerStateType.Normal:
					createdState = InstantiateState(typeof(NormalState));
					break;
				case PlayerStateType.Jumping:
					createdState = InstantiateState(typeof(JumpingState));
					break;
				case PlayerStateType.Crouching:
					createdState = InstantiateState(typeof(CrouchingState));
					break;
				case PlayerStateType.Death:
					createdState = InstantiateState(typeof(DeathState));
					break;
				default:
					createdState = InstantiateState(typeof(NormalState));
					break;
			}

			return createdState;
		}

		private PlayerState InstantiateState(Type type)
		{
			object[] ctorArgs =
			{
				_ninjaPlayer,
				_animator,
				_spriteRenderer,
				_rigidBody2D,
				_collider2D
			};

			var newState = Activator.CreateInstance(type,ctorArgs);
			
			return (PlayerState)newState;
		}
	}
}
