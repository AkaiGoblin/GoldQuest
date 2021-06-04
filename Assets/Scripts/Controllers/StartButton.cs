using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
	private TMP_Text _text;

	private void Awake()
	{
		_text = GetComponent<TMP_Text>();
	}
	private void OnMouseOver()
	{
		_text.outlineWidth = 1;
		_text.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 1);
	}

	private void OnMouseExit()
	{
		_text.outlineWidth = 0;
		_text.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0);
	}
	private void OnMouseDown()
	{
		SceneManager.LoadScene("Level1");
	}
}
