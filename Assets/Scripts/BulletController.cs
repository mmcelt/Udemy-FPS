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
		Destroy(gameObject);
		Instantiate(_impactEffect, transform.position, transform.rotation);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
