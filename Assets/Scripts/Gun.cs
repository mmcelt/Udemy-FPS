using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	#region Fields

	public GameObject _bullet;
	public Transform _firePoint;
	public bool _canAutoFire;
	public float _fireRate;
	[HideInInspector] public float _fireCounter;
	public int _currentAmmo;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (_fireCounter > 0)
			_fireCounter -= Time.deltaTime;
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
