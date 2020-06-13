using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
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
		transform.LookAt(PlayerController.Instance.transform.position);
		_theRB.velocity = transform.forward * _moveSpeed;
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
