using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
	#region Fields

	bool _collected;

	#endregion

	#region MonoBehaviour Methods

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !_collected)
		{
			_collected = true;

			AudioManager.Instance.PlaySFX(3);
			PlayerController.Instance._activeGun.GetAmmo();

			Destroy(gameObject);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
