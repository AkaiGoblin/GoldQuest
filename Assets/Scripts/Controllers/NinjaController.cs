using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Factories;
using Assets.Scripts.States;

public class NinjaController : MonoBehaviour
{
    #region Fields
    private Ninja _ninjaPlayer;
    private CommandFactory _commandFactory;
    private PlayerState _currentNinjaState;
    private float _horizontalInput;
    private bool _jump = false;    
    private bool _crouch;
    private float _slide;
	#endregion

	private void Start()
	{
        _ninjaPlayer = gameObject.GetComponent<Ninja>();
        _commandFactory = new CommandFactory(_ninjaPlayer);
	}

	private void Update()
	{
        ProcessHorizontalInput();
        ProcessJumpInput();
        ProcessCrouchInput();        

        var nextMove = _commandFactory.GetCommand(_horizontalInput, _jump, _crouch);
        nextMove.Execute();
        _jump = false;
	}

	private void ProcessCrouchInput()
	{
		if (Input.GetButton("Crouch") || Input.GetButtonDown("Crouch"))
		{
            _currentNinjaState = _ninjaPlayer.CurrentState;
			_crouch = true;
			return;
		}
		if (Input.GetButtonUp("Crouch") && _currentNinjaState.Equals(_ninjaPlayer.CurrentState))
		{
            _crouch = false;
            _ninjaPlayer.CurrentState.StoppedCrouchingDelegate();
        }
		else
		{
            _crouch = false;
		}
	}

	private void ProcessJumpInput()
    {
		if (Input.GetButtonDown("Jump"))
		{
            _jump = true;
		}
    }

    private void ProcessHorizontalInput()
    {
        var _horizontal = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(_horizontal) > Mathf.Epsilon)
        {
            _horizontalInput = _horizontal;
            return;
        }
        _horizontalInput = _horizontal;
    }
}
