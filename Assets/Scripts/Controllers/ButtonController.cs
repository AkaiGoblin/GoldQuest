using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
	private Animator _animator;
	private Collider2D _boxCollider;

	#region Delegates
	public delegate void ButtonPushedHandler();
	#endregion

	#region Events
	public event ButtonPushedHandler ButtonPushed;
	#endregion

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_boxCollider = GetComponent<BoxCollider2D>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<Ninja>() != null)
		{
			_animator.SetBool("ButtonPushed", true);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<Ninja>() != null)
		{
			_animator.SetBool("ButtonPushed", true);
			OnButtonPushed();
		}		
	}

	private void OnButtonPushed()
	{
		if (ButtonPushed!=null)
		{
			ButtonPushed();
		}
	}

	public void ButtonReset()
	{
		_animator.SetBool("ButtonPushed", false);
	}
}
