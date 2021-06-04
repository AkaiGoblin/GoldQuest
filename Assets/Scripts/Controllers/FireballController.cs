using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _lifeSpan;

	private Vector3 _initialPosition;
	private Vector3 _startPosition;

	private void Awake()
	{
		_initialPosition = transform.position;
		_startPosition = transform.position;
	}

	private void Update()
	{
		_initialPosition += Vector3.left * _speed * Time.deltaTime;
		transform.position = _initialPosition;
	}
}
