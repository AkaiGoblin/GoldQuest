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
		_gameManager = GameObject.FindObjectOfType<GameManager>();
		_gameManager.ScoreChanged += ScoreChanged;
		_scoreText = this.GetComponentInChildren<Text>();		
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
		_scoreText.text = $": {formattedValue}";		
	}
}
