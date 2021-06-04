using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
	[SerializeField]
	private float Length = 10f;
	[SerializeField]
	private float Speed = 1f;

	private float _initialPosition;
	private float _verticalPosition;
	private int _count = 0;
	private bool _up = true;

	private void Awake()
	{
		_verticalPosition = gameObject.transform.position.y;
		_initialPosition = _verticalPosition;
		if (Length < 0)
			_up = false;
	}

	private void Update()
	{
		transform.position = NewPosition();
	}

	private Vector3 NewPosition()
	{		
		if (_up)
		{
			_verticalPosition += Speed * Time.deltaTime;
			if (_verticalPosition >= _initialPosition + Mathf.Abs(Length/2))
			{
				_up = false;
			}
		}
		else
		{
			_verticalPosition -= Speed * Time.deltaTime;
			if (_verticalPosition <= _initialPosition - Mathf.Abs(Length / 2))
			{
				_up = true;
			}
		}
		return new Vector3(transform.position.x, _verticalPosition, 0);
	}
}
