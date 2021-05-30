using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LifeController : MonoBehaviour
{
	#region Game Variable
	[SerializeField]
	private GameObject _heartPrefab;
	[SerializeField]
	private Vector3 _defaultHeartPosition;
	[SerializeField]
	private RectTransform _defaultTest;
	public string hello;
	#endregion

	private List<GameObject> _hearts = new List<GameObject>();
	private GameManager _gameManager;
	private void Awake()
	{
		_gameManager = GameObject.FindObjectOfType<GameManager>();
		_gameManager.LifeChanged += PlayerIsHit;
		
	}
	private void Start()
	{
		GetPlayerLife();
	}

	private void PlayerIsHit(int currentLife)
	{
		var addOrRemoveHeart = _hearts.Count - currentLife;
		if (addOrRemoveHeart < 1)
		{
			Destroy(_hearts[_hearts.Count - 1]);
		}
	}

	private void GetPlayerLife()
	{
		var numberOfHearts = _gameManager.PlayerLife;
		CreateHeartImages(numberOfHearts);
	}

	private void CreateHeartImages(int numberOfHearts)
	{
		for (int i = 0; i < numberOfHearts; i++)
		{
			var position = i * 100 + 2;

			var test = Instantiate<GameObject>(_heartPrefab, this.transform.position, Quaternion.identity, this.transform);
			//var test = Instantiate<GameObject>(_heartPrefab);
			((RectTransform)test.transform).anchoredPosition = new Vector2(_defaultHeartPosition.x + position, _defaultHeartPosition.y);

			
		}		
	}

	private void OnDestroy()
	{
		_gameManager.LifeChanged -= PlayerIsHit;
	}
}
