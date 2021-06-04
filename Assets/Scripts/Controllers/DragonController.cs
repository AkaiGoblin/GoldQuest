using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
	[SerializeField]
	private GameObject _fireball;
	[SerializeField]
	private GameObject _coin;
	

	private Animator _animator;
	

	private void Awake()
	{
		_animator = this.GetComponent<Animator>();
	}

	

	public void SendFireBall() {

		_animator.SetBool("IsAttacking",true);
		StartCoroutine("SetFireball");

	}

	private IEnumerator SetFireball()
	{
		yield return new WaitForSeconds(0.02f);
		Instantiate(_fireball, this.transform.position, Quaternion.identity, transform);
	}
}
