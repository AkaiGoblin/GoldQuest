using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GoldCoin : MonoBehaviour
{
	 
	#region Game Variables
	public int CoinValue = 1;
	public AudioSource _sound;
	public AudioClip _clip;
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

	private bool _hasExploded = false;
	private Vector3 _startingPosition;
	private Vector3 _direction;
	private Renderer _renderer;

	
	

	private void Awake()
	{
		//_gameManager = GameObject.FindObjectOfType<GameManager>();
		
		
		_startingPosition = transform.position;
		_direction = new Vector3(Random.value, Random.value);
		_renderer = GetComponent<Renderer>();
	}
	private void Start()
	{
		_gameManager = GameManager.instance;
		GotRich += _gameManager.PutMoneyInWallet;
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			StartCoroutine("PlayAudioAndDie");
			OnAcquiredMoreMoney();
		}
	}

	private IEnumerator PlayAudioAndDie()
	{
		_sound.PlayOneShot(_clip);
		_renderer.enabled = false;
		yield return new WaitForSeconds(_clip.length);
		Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{

			_sound.PlayOneShot(_clip);
			OnAcquiredMoreMoney();
			
			Destroy(gameObject);
		}
		_hasExploded = true;
		
	}

	private void OnAcquiredMoreMoney()
	{
		if (GotRich != null)
		{
			GotRich(CoinValue);
		}
	}
}
