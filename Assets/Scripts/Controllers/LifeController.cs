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
	private int _separationDistance;
	#endregion

	private bool _lifeWasFetched = false;

	private List<GameObject> _hearts = new List<GameObject>();
	private GameManager _gameManager;
	private void Awake()
	{
		//_gameManager = GameObject.FindObjectOfType<GameManager>();
		
		
	}
	private void Start()
	{
		_gameManager = GameManager.instance;
		_gameManager.LifeChanged += PlayerIsHit;
		
	}
	private void Update()
	{
		if (!_lifeWasFetched)
		{
			GetPlayerLife();
		}
	}

	private void PlayerIsHit(int currentLife)
	{
		var addOrRemoveHeart = _hearts.Count - currentLife;
		if (addOrRemoveHeart >= 1)
		{
			var index = _hearts.Count - 1;
			var lossedHeart = _hearts[index];
			_hearts.RemoveAt(index);
			Destroy(lossedHeart);
		}

		if (addOrRemoveHeart < 1)
		{
			CreateHeartImages(1);
		}
	}

	private void GetPlayerLife()
	{
		var numberOfHearts = _gameManager.PlayerLife;
		CreateHeartImages(numberOfHearts);
		_lifeWasFetched = true;
	}

	private void CreateHeartImages(int numberOfHearts)
	{
		if (_hearts.Count == 0 || _hearts == null)
		{
			for (int i = 0; i < numberOfHearts; i++)
			{
				var position = i * _separationDistance;
				var heart = InstantiateHeartImage(position);
				_hearts.Add(heart);
			}
		}
		else
		{
			var position = _hearts.Count * _separationDistance;
			var heart = InstantiateHeartImage(position);
			_hearts.Add(heart);
		}
			
	}
	private GameObject InstantiateHeartImage(int position)
	{
		var heart = Instantiate<GameObject>(_heartPrefab, this.transform.position, Quaternion.identity, this.transform);
		((RectTransform)heart.transform).anchoredPosition = new Vector2(_defaultHeartPosition.x + position, _defaultHeartPosition.y);
		return heart;
	}

	private void OnDestroy()
	{
		_gameManager.LifeChanged -= PlayerIsHit;
	}
}
