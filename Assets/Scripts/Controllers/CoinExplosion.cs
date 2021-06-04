using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinExplosion : MonoBehaviour
{
	public GameObject Coin;
	[SerializeField]
	private float _force = 10000f;
	[SerializeField]
	private int _numberOfCoins = 5;

	private void Update()
	{

		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			ExplosiveEntry();
		}
	}

	private void ExplosiveEntry()
	{

		for (int i = 0; i < _numberOfCoins; i++)
		{
			var newCoin = Instantiate(Coin, this.transform, false);
			//newCoin.transform.position = transform.position;

			//var randomVector2 = new Vector2(Random.value, Random.value) * _force;

			//newCoin.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _force);

		}
	}
}
