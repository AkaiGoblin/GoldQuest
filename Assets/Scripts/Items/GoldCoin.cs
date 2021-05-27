using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{

	#region Game Variables
	public int CoinValue = 1;
	#endregion

	#region Fields
	private GameManager _gameManager;
	#endregion

	#region Delegates
	public delegate void GotRicherHandler(int amount);
	#endregion

	#region Events
	public event GotRicherHandler GotRich;
	#endregion


	private void Awake()
	{
		_gameManager = GameObject.FindObjectOfType<GameManager>();
		GotRich += _gameManager.PutMoneyInWallet;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		OnAcquiredMoreMoney();
		Destroy(gameObject);
	}

	private void OnAcquiredMoreMoney()
	{
		if (GotRich != null)
		{
			GotRich(CoinValue);
		}
	}
}
