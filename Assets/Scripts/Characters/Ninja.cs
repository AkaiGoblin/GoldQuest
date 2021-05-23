using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{
	#region Game Variable
	[SerializeField]
	private float _speed = 5f;
	#endregion

	#region Fields
	private Animator _animator;
	private Transform _transform;
	#endregion

	private void Start()
	{
		_animator = gameObject.GetComponent<Animator>();
		_transform = gameObject.GetComponent<Transform>();
	}

	#region Movement Methods

	public void MoveRight()
	{
		_transform.position = _transform.position + Move(Vector3.right);
	}

	public void MoveLeft()
	{
		_transform.position = _transform.position + Move(Vector3.left);
	}

	public void Jump()
	{

	}

	public void Crouch()
	{

	}

	public void Slide()
	{

	}

	private Vector3 Move(Vector3 direction)
	{
		return _speed * Time.deltaTime * direction;
	}


	#endregion


}
