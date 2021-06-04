using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextOnFinishController : MonoBehaviour
{
	private TMP_Text _text;
	private GameManager _gameManager;

	private void Awake()
	{
		_text = GetComponent<TMP_Text>();
	}

	private void Start()
	{
		_gameManager = GameManager.instance;
	}
	private void Update()
	{
		if(!string.IsNullOrEmpty(_gameManager.LastScene) && _gameManager.LastScene.Equals("Level5"))
		{
			_text.enabled = true;
		}
	}
}
