using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	#region Fields

	[SerializeField] float _moveSpeed, _lifetime;
	[SerializeField] Rigidbody _theRB;
	[SerializeField] GameObject _impactEffect;
	[SerializeField] int _bulletDamage = 1;
	[SerializeField] bool _damageEnemy, _damagePlayer;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		Destroy(gameObject, _lifetime);
	}
	
	void Update() 
	{
		_theRB.velocity = transform.forward * _moveSpeed;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy") && _damageEnemy)
		{
			//Destroy(other.gameObject);
			other.GetComponent<EnemyHealthController>().DamageEnemy(_bulletDamage);
		}
		if (other.CompareTag("Player") && _damagePlayer)
		{
			Debug.Log("PLAYER HIT AT " + transform.position);
		}

		Destroy(gameObject);
		//move the instantiation position back 1 frame in position
		Instantiate(_impactEffect, transform.position + (transform.forward * (-_moveSpeed * Time.deltaTime)), transform.rotation);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
