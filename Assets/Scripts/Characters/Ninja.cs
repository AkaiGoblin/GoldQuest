using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Scripts.States;
using UnityEngine;
using Assets.Scripts.Factories;
using UnityEngine.SceneManagement;

public class Ninja : MonoBehaviour
{
	#region Delegates
	public delegate void PlayerStateChangeHandler(PlayerState newState);
	public delegate void PlayerDeathHandler();
	public delegate void GameObjectDestroyedHandler();
	#endregion

	#region Events
	public event PlayerDeathHandler PlayerIsDead;
	public event GameObjectDestroyedHandler GameObjectDestroyed;
	#endregion

	#region Game Variable
	public float Speed = 5f;
	public float JumpSpeed = 10f;
	public float SlideTimer = 2.5f;
	public float SpeedModifier = 1f;
	public int Life = 3;
	#endregion

	#region Fields
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	private Rigidbody2D _rigidBody2D;
	private CapsuleCollider2D _capsuleCollider;
	private CollisionController _collisionController;
	private GameManager _gameManager;
	private PlayerStateFactory _stateFactory;
	private GameObject _spawnPoint;
	private LevelController _levelManager;
	private bool _wasHit = false;
	private bool _godMode = false;
	#endregion

	#region Non Serialized Properties
	[NonSerialized]
	public PlayerState CurrentState;
	public string PlayerCurrentState;
	[NonSerialized]
	public PlayerStateChangeHandler StateChangeDelegate;
	#endregion


	private void Awake()
	{
		//Instantiation
		_animator = gameObject.GetComponent<Animator>();
		_rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		_capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
		_collisionController = gameObject.GetComponent<CollisionController>();
		//_gameManager = FindObjectOfType<GameManager>();
		
		_stateFactory = PlayerStateFactory.GetInstance();
		_spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
		_levelManager = GameObject.FindObjectOfType<LevelController>();

		//Assignation		
		CurrentState = _stateFactory.CreatePlayerState(PlayerStateType.Normal);
		StateChangeDelegate = new PlayerStateChangeHandler(PlayerStateChanged);
		_collisionController.PlayerIsHit += PlayerLooseLife;
		
		PlayerIsDead += PlayerDies;
		_levelManager.LevelFinished += LevelFinished;
	}
	private void Start()
	{
		_godMode = false;
		_gameManager = GameManager.instance;
		if (_gameManager.PlayerLife == 0)
		{
			_gameManager.PlayerLife = Life;
		}

		if (_wasHit)
		{
			gameObject.transform.position = _spawnPoint.transform.position;
			_wasHit = false;
		}
		
		_gameManager.GetInitialPlayerWallet();
		
	}
	private void Update()
	{
		PlayerCurrentState = CurrentState.ToString();
	}
	

	#region Movement Methods

	public void MoveRight()
	{
		if (CurrentState == null)
			return;
		CurrentState.MoveRight();
	}

	public void MoveLeft()
	{
		if (CurrentState == null)
			return;
		CurrentState.MoveLeft();
	}

	public void Jump()
	{
		if (CurrentState == null)
			return;
		CurrentState.Jump();
	}

	public void Crouch()
	{
		if (CurrentState == null)
			return;
		CurrentState.Crouch();
	}

	public void Idle()
	{
		if (CurrentState == null)
			return;
		CurrentState.Idle();
	}

	#endregion

	private void PlayerStateChanged(PlayerState newState)
	{
		CurrentState = newState;
	}

	private void PlayerLooseLife(int hitPoint)
	{
		_wasHit = true;
		if (_godMode == true)
		{
			hitPoint = 0;
		}
		Life -= hitPoint;
		_gameManager.PlayerGetsHit(hitPoint);
		if (Life == 0)
		{
			PlayerDies();
			return;
		}

		PlayerRespawnProcess();

	}

	private void PlayerRespawnProcess()
	{
		_capsuleCollider.enabled = false;
		PlayerStateChanged(_stateFactory.CreatePlayerState(PlayerStateType.Death));
		_animator.SetBool("IsDead", true);
		PlayerTurnRed();
		StartCoroutine(PlayerDisapears());
		Invoke("PlayerRespawn", 2);
	}

	private void PlayerRespawn()
	{
		var spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
		gameObject.transform.position = spawnPoint.transform.position;
		_spriteRenderer.color = Color.white;
		_animator.SetBool("IsDead", false);
		PlayerStateChanged(_stateFactory.CreatePlayerState(PlayerStateType.Normal));
		_capsuleCollider.enabled = true;
	}

	private void PlayerTurnRed()
	{
		var currentColor = _spriteRenderer.color;
		float interpolation = 0f;
		while (_spriteRenderer.color != Color.red)
		{
			_spriteRenderer.color = Color.Lerp(currentColor, Color.red, interpolation);
			interpolation += 0.01f;
		}
		
	}
	private IEnumerator PlayerDisapears()
	{
		var currentColor = _spriteRenderer.color;		
		for (float i = 0.9f; i >= -0.1f; i -= 0.1f)
		{
			if (i>0)
			{
				currentColor.a = i;
				_spriteRenderer.color = currentColor;
			}
			else
			{
				currentColor.a = 0;
				_spriteRenderer.color = currentColor;
			}
			yield return new WaitForSeconds(0.05f);
		}
		
	}

	public void PlayerDies()
	{
		_gameManager.GameOver();
		
	}

	private void LevelFinished(Scene activeScene, bool isDead)
	{
		Destroy(gameObject);
	}

	private void OnDestroy()
	{
		_collisionController.PlayerIsHit -= PlayerLooseLife;
		PlayerIsDead -= PlayerDies;
		_levelManager.LevelFinished -= LevelFinished;
	}

}
