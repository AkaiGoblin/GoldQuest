using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
	#region Delegates
	public delegate void LevelFinishedHandler(Scene activeScene, bool isDead);
	#endregion

	#region Events
	public event LevelFinishedHandler LevelFinished;
	#endregion

	private GameManager _gameManager;
	private void Awake()
	{
		//_gameManager = GameObject.FindObjectOfType<GameManager>();	
		
	}

	private void Start()
	{
		_gameManager = GameManager.instance;
		LevelFinished += _gameManager.LevelFinished;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(collision.gameObject);
		LevelFinished(SceneManager.GetActiveScene(), false);
	}

	private void OnDestroy()
	{
		LevelFinished -= _gameManager.LevelFinished;
	}
}
