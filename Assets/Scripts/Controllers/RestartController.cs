using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestartController : MonoBehaviour
{
	private GameManager _gameManager;
	private TMP_Text _restartText;
	//private TMP_ _shader;
	

	private void Awake()
	{
		//_gameManager = FindObjectOfType<GameManager>();
		
		//_shader = GetComponent<Shader>();
		_restartText = GetComponent<TMP_Text>();
	}

	private void Start()
	{
		_gameManager = GameManager.instance;
	}

	private void OnMouseOver()
	{
		_restartText.outlineWidth = 1;
		_restartText.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 1);
	}

	private void OnMouseExit()
	{
		_restartText.outlineWidth = 0;
		_restartText.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0);
	}

	private void OnMouseDown()
	{
		_gameManager.GameRestart();
	}


}
