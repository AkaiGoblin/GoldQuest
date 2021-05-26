using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Scripts.States;
using UnityEngine;

public class Ninja : MonoBehaviour
{
	#region Delegates
	public delegate void PlayerStateChangeHandler(PlayerState newState);
	#endregion

	#region Game Variable
	[SerializeField]
	public float Speed = 5f;
	[SerializeField]
	public float JumpSpeed = 10f;
	[SerializeField]
	public float SlideTimer = 2.5f;
	[NonSerialized]
	public float SpeedModifier = 1f;
	#endregion

	#region Fields
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	private Rigidbody2D _rigidBody2D;
	private CapsuleCollider2D _capsuleCollider;
	#endregion

	#region Non Serialized Properties
	[NonSerialized]
	public PlayerState CurrentState;
	public string PlayerCurrentState;
	[NonSerialized]
	public PlayerStateChangeHandler StateChangeDelegate;
	#endregion


	private void Awake()
	{
		_animator = gameObject.GetComponent<Animator>();		
		_rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();		
		_capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
		CurrentState = new NormalState(this, _animator, _spriteRenderer, _rigidBody2D, _capsuleCollider);
		StateChangeDelegate = new PlayerStateChangeHandler(PlayerStateChanged);
	}
	private void Update()
	{
		PlayerCurrentState = CurrentState.ToString();
	}

	#region Movement Methods

	public void MoveRight()
	{
		CurrentState.MoveRight();
	}

	public void MoveLeft()
	{
		CurrentState.MoveLeft();		
	}

	public void Jump()
	{
		CurrentState.Jump();
	}

	public void Crouch()
	{
		CurrentState.Crouch();
	}

	public void Idle()
	{
		CurrentState.Idle();
	}

	#endregion

	private void PlayerStateChanged(PlayerState newState)
	{
		CurrentState = newState;
	}

}
