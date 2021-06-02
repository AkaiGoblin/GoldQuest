using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public int PlayerLife { get => _playerLife; set => _playerLife = value; }

	[SerializeField]
	private int _playerWallet;
	private int _playerLife;


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

	public void LevelFinished(string sceneName = null)
	{
		
		if (!String.IsNullOrEmpty(sceneName))
		{
			SceneManager.LoadScene("GameOver");
			return;
		}
		SceneManager.LoadScene(_indexFirstScene++);
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

	

}
