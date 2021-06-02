using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	#region Delegates
	public delegate void LevelFinishedHandler(string sceneName = null);
	#endregion

	#region Events
	public event LevelFinishedHandler LevelFinished;
	#endregion

	private GameManager _gameManager;
	private void Awake()
	{
		_gameManager = GameObject.FindObjectOfType<GameManager>();	
	}

	private void Start()
	{
		LevelFinished += _gameManager.LevelFinished;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(collision.gameObject);
		LevelFinished();
	}

	private void OnDestroy()
	{
		LevelFinished -= _gameManager.LevelFinished;
	}
}
