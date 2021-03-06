using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Factories;
using Assets.Scripts.States;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
	#region Delegates
	public delegate void PlayerIsHitHandler(int hitPoint);
	#endregion

	#region Events
	public event PlayerIsHitHandler PlayerIsHit;
	#endregion

	private Ninja _ninjaPlayer;
	private SpriteRenderer _spriteRenderer;
	private Animator _ninjaAnimator;
	private Rigidbody2D _rigidBody2D;
	private CapsuleCollider2D _capsuleCollider;
	private PlayerStateFactory _stateFactory;

	private void Awake()
	{
		_ninjaPlayer = gameObject.GetComponent<Ninja>();
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		_ninjaAnimator = gameObject.GetComponent<Animator>();
		_rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
		_capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
		_stateFactory = PlayerStateFactory.GetInstance();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		GroundCollisionCheck(collision);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		SpikeCollisionCheck(collision);
		FireballCollisionCheck(collision);
	}

	private void SpikeCollisionCheck(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Spike"))
		{
			PlayerIsHit(1);
		}
	}

	private void FireballCollisionCheck(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Fireball"))
		{
			_ninjaPlayer.PlayerDies();
		}
	}

	private void GroundCollisionCheck(Collision2D collision)
	{		
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") &&
			_ninjaPlayer.CurrentState is JumpingState)
		{
			_ninjaAnimator.SetBool("IsJumping", false);
			_ninjaPlayer.StateChangeDelegate(
				_stateFactory.CreatePlayerState(PlayerStateType.Normal)
				);
		}
	}
}
