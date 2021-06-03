using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
	[SerializeField]
	private float _speed;

	private Vector3 _initialPosition;
	private Vector3 _startPosition;

	private void Awake()
	{
		_initialPosition = transform.position;
		_startPosition = transform.position;
	}


}
