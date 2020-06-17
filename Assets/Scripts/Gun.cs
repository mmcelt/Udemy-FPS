using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	#region Fields

	public string _gunName;
	public GameObject _bullet;
	public Transform _firePoint;
	public bool _canAutoFire;
	public float _fireRate;
	[HideInInspector] public float _fireCounter;
	public int _currentAmmo, _pickupAmount;
	public float _zoomAmount;

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

	public void GetAmmo()
	{
		_currentAmmo += _pickupAmount;
		UpdateAmmoUI();
	}
	#endregion

	#region Private Methods

	void UpdateAmmoUI()
	{
		UIController.Instance._ammoText.text = "AMMO: " + _currentAmmo;
	}
	#endregion
}
