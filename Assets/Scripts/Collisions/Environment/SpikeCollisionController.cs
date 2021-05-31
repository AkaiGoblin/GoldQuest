using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollisionController : MonoBehaviour
{
	private float _repulsiveForce = 5f;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var direction3D = (collision.transform.position - transform.position).normalized;
		var direction2D = new Vector2(direction3D.x, direction3D.y);
		var playerRigibBody = collision.attachedRigidbody;
		playerRigibBody.AddForce(direction2D * _repulsiveForce, ForceMode2D.Impulse);
	}
}
