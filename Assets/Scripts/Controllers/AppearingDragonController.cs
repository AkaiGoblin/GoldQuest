using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingDragonController : MonoBehaviour
{
	private Animator _explosionAnimator;
	private GameObject _goldStack;
	private DragonController _dragon;
	private bool _exploded = false;
	private AudioSource _audioSource;

	private void Awake()
	{
		_explosionAnimator = GameObject.Find("Explosion").GetComponent<Animator>();
		_goldStack = GameObject.Find("GoldStack");
		_dragon = FindObjectOfType<DragonController>();
		_audioSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !_exploded)
		{
			_explosionAnimator.SetTrigger("IsExploding");
			_audioSource.Play();
			_goldStack.SetActive(false);
			StartCoroutine("Fireball");
			//_dragon.SendFireBall();
			_exploded = true;
		}
	}
	private IEnumerator Fireball()
	{
		yield return new WaitForSeconds(1f);

		_dragon.SendFireBall();
	}
}
