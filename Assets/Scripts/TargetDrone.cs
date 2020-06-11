using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDrone : MonoBehaviour
{
	#region Fields
	[Header("Orbiter")]
	[SerializeField] bool _orbiter;
	[SerializeField] float _orbitDistance, _orbitSpeed;
	[SerializeField] Transform _orbitPoint;
	[Header("Vertical Mover")]
	[SerializeField] bool _vertMover;
	[SerializeField] Transform _centerPoint;
	[SerializeField] Vector3 _lowerPoint, _upperPoint;
	[SerializeField] float _vMoveSpeed;
	bool _reachedLowerPoint, _reachedUpperPoint;
	[Header("Mover")]
	[SerializeField] bool _mover;
	[SerializeField] float _hMoveSpeed;


	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		if (_vertMover)
		{
			transform.position = _centerPoint.position;
			_reachedUpperPoint = true;
		}
	}
	
	void Update() 
	{
		if (_orbiter)
		{
			transform.RotateAround(_orbitPoint.position, Vector3.up, _orbitSpeed * Time.deltaTime);
		}
		if (_vertMover)
		{
			if(!_reachedLowerPoint)
				MoveDown();
			if (!_reachedUpperPoint)
				MoveUp();
		}
		if (_mover)
			transform.position += new Vector3(_hMoveSpeed, 0f, 0f) * Time.deltaTime;
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void MoveDown()
	{
		transform.position = Vector3.MoveTowards(transform.position, _lowerPoint, _vMoveSpeed * Time.deltaTime);
		if(Vector3.Distance(transform.position, _lowerPoint) <= Mathf.Abs( 0.1f))
		{
			_reachedLowerPoint = true;
			_reachedUpperPoint = false;
		}
	}

	void MoveUp()
	{
		transform.position = Vector3.MoveTowards(transform.position, _upperPoint, _vMoveSpeed * Time.deltaTime);
		if (Vector3.Distance(transform.position, _upperPoint) <= Mathf.Abs(0.1f))
		{
			_reachedLowerPoint = false;
			_reachedUpperPoint = true;
		}
	}
	#endregion
}
