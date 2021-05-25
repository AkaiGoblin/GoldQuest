using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.States;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
	private Ninja _ninjaPlayer;
	private SpriteRenderer _spriteRenderer;
	private Animator _ninjaAnimator;
	private Rigidbody2D _rigidBody2D;
	private CapsuleCollider2D _capsuleCollider;

	private void Awake()
	{
		_ninjaPlayer = gameObject.GetComponent<Ninja>();
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		_ninjaAnimator = gameObject.GetComponent<Animator>();
		_rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
		_capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		GroundCollisionCheck(collision);
	}

	private void GroundCollisionCheck(Collision2D collision)
	{		
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") &&
			_ninjaPlayer.CurrentState is JumpingState)
		{
			_ninjaAnimator.SetBool("IsJumping", false);
			_ninjaPlayer.StateChangeDelegate(new NormalState(
				_ninjaPlayer,
				_ninjaAnimator,
				_spriteRenderer,
				_rigidBody2D,
				_capsuleCollider));
		}
	}
}
