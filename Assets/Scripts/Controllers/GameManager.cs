using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[SerializeField]
	private int _playerWallet;

	#region Delegates
	public delegate void ScoreChangedHandler(int newScore);
	#endregion

	#region Event
	public event ScoreChangedHandler ScoreChanged;
	#endregion

	private void Awake()
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
	private void Start()
	{
		
	}

	public void PutMoneyInWallet(int value)
	{
		_playerWallet += value;
		OnScoreChanged();
	}

	private void OnScoreChanged()
	{
		if (ScoreChanged != null)
		{
			ScoreChanged(_playerWallet);
		}
	}

	private void OnLevelWasLoaded(int level)
	{
		AddScoreController();
	}

	private void AddScoreController()
	{
		
	}

}
