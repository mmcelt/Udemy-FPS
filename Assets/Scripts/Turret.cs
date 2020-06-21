using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _bullet;
	[SerializeField] float _rangeToTarget, _timeBetweenShots = 0.5f, _rotationSpeed = 45f;
	[SerializeField] Transform _gun, _portFirePoint, _stbdFirePoint;

	float _shotCounter;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_shotCounter = _timeBetweenShots;
	}
	
	void Update() 
	{
		if (GameManager.Instance._levelEnding) return;

		if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < _rangeToTarget)
		{
			_gun.LookAt(PlayerController.Instance.transform.position + new Vector3(0f, 1.2f, 0f));

			_shotCounter -= Time.deltaTime;

			if (_shotCounter <= 0)
			{
				Instantiate(_bullet, _portFirePoint.position, _portFirePoint.rotation);
				Instantiate(_bullet, _stbdFirePoint.position, _stbdFirePoint.rotation);
				_shotCounter = _timeBetweenShots;
			}
		}
		else
		{
			_shotCounter = _timeBetweenShots;
			_gun.rotation = Quaternion.Lerp(_gun.rotation, Quaternion.Euler(0f, _gun.rotation.eulerAngles.y + 10f, 0f), _rotationSpeed * Time.deltaTime);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
