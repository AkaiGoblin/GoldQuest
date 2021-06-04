using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{

	private bool _canMove = false;
	private ButtonController _button;
	private Vector3 _initialPosition;
	private Vector3 _startingPosition;
	private CollisionController _ninjaCollisionController;


	[SerializeField]
	private float _finalXPosition;
	[SerializeField]
	private float _speed;

	private void Awake()
	{
		_button = FindObjectOfType<ButtonController>();
		_button.ButtonPushed += CanMove;
		_initialPosition = transform.position;
		_startingPosition = transform.position;
		_ninjaCollisionController = FindObjectOfType<CollisionController>();
		_ninjaCollisionController.PlayerIsHit += PlayerHitReset;
	}

	private void Update()
	{
		if (_canMove)
		{
			_initialPosition += Vector3.right * _speed * Time.deltaTime;
			transform.position = _initialPosition;

			if (transform.position.x >= _finalXPosition)
			{
				_canMove = false;
			}
		}
	}

	private void CanMove()
	{
		_canMove = true;
	}

	private void PlayerHitReset(int hit)
	{
		transform.position = _startingPosition;
		_button.ButtonReset();
		_canMove = false;
		_initialPosition = _startingPosition;
	}
}
