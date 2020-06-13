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
	[SerializeField] float _distanceToChase = 10f, _distanceToLose = 15f, _distanceToStop = 2f;
	[SerializeField] float _keepChasingTime = 5f;

	bool _chasing;
	Vector3 _targetPoint, _startPoint;
	float _chaseCounter;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_theAgent.speed = _moveSpeed;
		_startPoint = transform.position;
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

			if(_chaseCounter > 0)
			{
				_chaseCounter -= Time.deltaTime;

				if (_chaseCounter <= 0)
				{
					_theAgent.destination = _startPoint;
				}
			}
		}
		else
		{
			//transform.LookAt(_targetPoint);
			//_theRB.velocity = transform.forward * _moveSpeed;
			if(Vector3.Distance(transform.position, _targetPoint) > _distanceToStop)
			{
				_theAgent.destination = _targetPoint;
			}
			else
			{
				_theAgent.destination = transform.position;
			}

			//the enemy has lost the player...
			if (Vector3.Distance(transform.position, _targetPoint) >= _distanceToLose)
			{
				_chasing = false;

				_chaseCounter = _keepChasingTime;

			}
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
