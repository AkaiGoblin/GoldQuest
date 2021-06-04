using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public int PlayerLife { get => _playerLife; set => _playerLife = value; }
	public int PlayerWallet { get => _playerWallet; set => _playerWallet = value; }
	public string LastScene { get => _lastScene; set => _lastScene = value; }

	[SerializeField]
	private int _playerWallet;
	private int _playerLife;
	private string _lastScene;
	private AudioSource _audioSource;


	#region Fields
	private int _indexFirstScene = 1;
	#endregion

	#region Delegates
	public delegate void ScoreChangedHandler(int newScore);
	public delegate void LifeChangedHandler(int life);
	
	#endregion

	#region Event
	public event ScoreChangedHandler ScoreChanged;
	public event LifeChangedHandler LifeChanged;	
	#endregion

	private void Awake()
	{
		ImplementSingleton();
		_audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (!SceneManager.GetActiveScene().name.Equals("GameOver"))
		{
			_lastScene = SceneManager.GetActiveScene().name;
		}
		if (!_audioSource.isPlaying && 
			!SceneManager.GetActiveScene().name.Equals("GameOver") &&
			!SceneManager.GetActiveScene().name.Equals("Start"))
		{
			_audioSource.Play();
		}

		if (SceneManager.GetActiveScene().name.Equals("GameOver"))
		{
			_audioSource.Stop();
		}
	}
	public void GetInitialPlayerWallet()
	{
		OnScoreChanged();
	}
	public void PutMoneyInWallet(int value)
	{
		_playerWallet += value;
		OnScoreChanged();
	}

	public void PlayerGetsHit(int heart)
	{
		_playerLife -= heart;
		OnLifeChanged();
	}

	public void LevelFinished(Scene activeScene, bool isDead)
	{

		if (isDead)
		{
			SceneManager.LoadScene("GameOver");
			return;
		}
		var sceneIndex = activeScene.buildIndex + 1;		
		SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
	}
	private void ImplementSingleton()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	private void OnLifeChanged()
	{
		if (_playerLife == 0)
		{
			GameOver();
			return;
		}
		if (LifeChanged != null)
		{
			LifeChanged(_playerLife);
		}
	}

	private void OnScoreChanged()
	{
		if (ScoreChanged != null)
		{
			ScoreChanged(_playerWallet);
		}
	}


	public void GameRestart()
	{
		_playerWallet = 0;
		SceneManager.LoadScene("Level1");

	}

	public void GameOver()
	{
		SceneManager.LoadScene("GameOver");
	}
	

}
