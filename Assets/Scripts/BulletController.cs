using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	#region Fields

	[SerializeField] float _moveSpeed;
	[SerializeField] Rigidbody _theRB;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		_theRB.velocity = transform.forward * _moveSpeed;
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
