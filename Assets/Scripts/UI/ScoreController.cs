using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private GameManager _gameManager;
	private Text _scoreText;



	private void Awake()
	{
		//_gameManager = GameObject.FindObjectOfType<GameManager>();
		
				
	}
	private void Start()
	{
		_scoreText = this.GetComponentInChildren<Text>();
		_gameManager = GameManager.instance;
		_gameManager.ScoreChanged += ScoreChanged;
		ScoreChanged(_gameManager.PlayerWallet);
	}

	private void ScoreChanged(int newValue)
	{
		StringBuilder formattedValue = new StringBuilder(); ;
		if (newValue < 10)
		{
			formattedValue.Append("00" + newValue);
		}
		else if (newValue < 100)
		{
			formattedValue.Append("0" + newValue);
		}
		else
		{
			formattedValue.Append(newValue);
		}
		if (_scoreText == null)
		{
			_scoreText = this.GetComponentInChildren<Text>();
		}
		_scoreText.text = $": {formattedValue}";		
	}

	private void OnDestroy()
	{
		_gameManager.ScoreChanged -= ScoreChanged;
	}
}
