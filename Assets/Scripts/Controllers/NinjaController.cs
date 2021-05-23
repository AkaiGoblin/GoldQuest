using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Factories;

public class NinjaController : MonoBehaviour
{
    #region Fields
    private Ninja _ninjaPlayer;
    private CommandFactory _commandFactory;
    private float _verticalInput;
    private float _horizontalInput;
    private float _jump;
    private float _crouch;
    private float _slide;
	#endregion

	private void Start()
	{
        _ninjaPlayer = gameObject.GetComponent<Ninja>();
        _commandFactory = new CommandFactory(_ninjaPlayer);
	}

	private void Update()
	{
        ProcessInput();
        //MoveHorizontally();
        var nextMove = _commandFactory.GetCommand(_horizontalInput, _verticalInput);
        nextMove.Execute();
	}

    private void MoveHorizontally()
	{
		if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon) return;

		if (_horizontalInput > Mathf.Epsilon)
		{
            _ninjaPlayer.MoveRight();
            return;
		}

        _ninjaPlayer.MoveLeft();
	}

	private void ProcessInput()
    {
        var _horizontal = Input.GetAxisRaw("Horizontal");
        var _vertical = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(_horizontal) > Mathf.Epsilon)
        {
            _horizontalInput = _horizontal;
            _verticalInput = 0f;
            return;
        }
        if (Mathf.Abs(_vertical) > Mathf.Epsilon)
        {
            _horizontalInput = 0f;
            _verticalInput = _vertical;
            return;
        }

        _horizontalInput = _horizontal;
        _verticalInput = _vertical;
    }
}
