using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
	private GameManager _gameManager;
	private Text _score;

	private void Awake()
	{
		//_gameManager = FindObjectOfType<GameManager>();
		
		_score = GetComponent<Text>();
	}
	

	private void Start()
	{
		_gameManager = GameManager.instance;
		_score.text = _gameManager.PlayerWallet.ToString();
	}
}
