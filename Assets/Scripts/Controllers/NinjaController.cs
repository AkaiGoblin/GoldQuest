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

    private PlayerStateFactory _stateFactory;

    #region readonly Axis
    private readonly string _horizontalAxis = "Horizontal";
    private readonly string _jumpAxis = "Jump";
    private readonly string _crouchAxis = "Crouch";
    #endregion


    private void Start()
	{
        _ninjaPlayer = gameObject.GetComponent<Ninja>();
        _commandFactory = new CommandFactory(_ninjaPlayer);
        _ninjaPlayer.PlayerIsDead += DeathSequence;
        _stateFactory = PlayerStateFactory.GetInstance();
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
        if (_ninjaPlayer.CurrentState is JumpingState) return;
		if (Input.GetButton(_crouchAxis) || Input.GetButtonDown(_crouchAxis))
		{
            _currentNinjaState = _ninjaPlayer.CurrentState;
			_crouch = true;
			return;
		}
		if (Input.GetButtonUp(_crouchAxis) && _currentNinjaState.Equals(_ninjaPlayer.CurrentState))
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
		if (Input.GetButtonDown(_jumpAxis))
		{
            _jump = true;
		}
    }

    private void ProcessHorizontalInput()
    {
        var _horizontal = Input.GetAxisRaw(_horizontalAxis);
        if (Mathf.Abs(_horizontal) > Mathf.Epsilon)
        {
            _horizontalInput = _horizontal;
            return;
        }
        _horizontalInput = _horizontal;
    }

    private void DeathSequence()
	{

	}

	private void OnDestroy()
	{
        _ninjaPlayer.PlayerIsDead -= DeathSequence;
	}
}
