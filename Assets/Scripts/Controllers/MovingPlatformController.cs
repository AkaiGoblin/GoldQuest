using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
	[SerializeField]
	private float _speed = 1f;
	[SerializeField]
	private float _finalYPosition;
	private Vector3 _initialPosition;

	private void Awake()
	{
		_initialPosition = transform.position;
	}

	private void Update()
	{
		if (transform.position.y >= _finalYPosition)
			return;
		_initialPosition += Vector3.up * _speed * Time.deltaTime;
		transform.position = _initialPosition;
	}



}
