using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	#region Fields

	[SerializeField] NavMeshAgent _theAgent;
	[SerializeField] Animator _theAnim;

	[SerializeField] float _distanceToChase = 10f, _distanceToLose = 15f, _distanceToStop = 2f;
	[SerializeField] float _keepChasingTime = 5f;
	[SerializeField] GameObject _bulletPrefab;
	[SerializeField] Transform _firePoint;
	[SerializeField] float _fireRate, _waitBetweenShots = 2f, _timeToShoot = 1f;

	float _fireCounter, _shotWaitCounter, _shootTimeCounter;

	bool _chasing;
	Vector3 _targetPoint, _startPoint;
	float _chaseCounter;

	#endregion

	#region MonoBehaviour Methods

	void Start()
	{
		_startPoint = transform.position;
		_shootTimeCounter = _timeToShoot;
		_shotWaitCounter = _waitBetweenShots;
	}

	void Update()
	{
		if (!PlayerController.Instance.gameObject.activeInHierarchy) return;

		_targetPoint = PlayerController.Instance.transform.position;
		_targetPoint.y = transform.position.y;

		if (!_chasing)
		{
			if (Vector3.Distance(transform.position, _targetPoint) < _distanceToChase)
			{
				_chasing = true;
				_shootTimeCounter = _timeToShoot;
				_shotWaitCounter = _waitBetweenShots;
			}

			if (_chaseCounter > 0)
			{
				_chaseCounter -= Time.deltaTime;

				if (_chaseCounter <= 0)
				{
					_theAgent.destination = _startPoint;
				}
			}

			//check if stopping...
			if (_theAgent.remainingDistance < 0.25f)
			{
				_theAnim.SetBool("isMoving", false);
			}
			else
			{
				_theAnim.SetBool("isMoving", true);
			}
		}
		else
		{
			//CHASING THE PLAYER...

			//transform.LookAt(_targetPoint);
			//_theRB.velocity = transform.forward * _moveSpeed;
			if (Vector3.Distance(transform.position, _targetPoint) > _distanceToStop)
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

			//moving & waiting to shoot...
			if (_shotWaitCounter > 0)
			{
				_shotWaitCounter -= Time.deltaTime;

				if (_shotWaitCounter <= 0)
				{
					_shootTimeCounter = _timeToShoot;
				}

				_theAnim.SetBool("isMoving", true);
			}
			else
			{
				_shootTimeCounter -= Time.deltaTime;

				if (_shootTimeCounter > 0)
				{
					_fireCounter -= Time.deltaTime;

					if (_fireCounter <= 0)
					{
						_fireCounter = _fireRate;

						_firePoint.LookAt(PlayerController.Instance.transform.position + new Vector3(0f, 1.2f, 0f));

						//check the angle to the player
						Vector3 targetDirection = PlayerController.Instance.transform.position - transform.position;
						float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);

						if (Mathf.Abs(angle) < 30f)
						{
							_theAnim.SetTrigger("fireShot");
							Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
						}
						else
						{
							_shotWaitCounter = _waitBetweenShots;
						}
					}
					//stop & shoot...
					_theAgent.destination = transform.position;
				}
				else
				{
					_shotWaitCounter = _waitBetweenShots;
				}


				//_theAgent.destination = transform.position;
				//_theAnim.SetBool("isMoving", false);
				_theAnim.SetBool("isMoving", false);

			}
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
