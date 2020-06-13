using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	#region Fields

	[SerializeField] float _moveSpeed;
	[SerializeField] Rigidbody _theRB;
	[SerializeField] NavMeshAgent _theAgent;
	[SerializeField] float _distanceToChase = 10f, _distanceToLose = 15f;

	bool _chasing;
	Vector3 _targetPoint;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		_targetPoint = PlayerController.Instance.transform.position;
		_targetPoint.y = transform.position.y;

		if (!_chasing)
		{
			if(Vector3.Distance(transform.position, _targetPoint) < _distanceToChase)
			{
				_chasing = true;
			}
		}
		else
		{
			//transform.LookAt(_targetPoint);
			//_theRB.velocity = transform.forward * _moveSpeed;
			_theAgent.destination = _targetPoint;

			if(Vector3.Distance(transform.position, _targetPoint) >= _distanceToLose)
			{
				_chasing = false;
			}
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
