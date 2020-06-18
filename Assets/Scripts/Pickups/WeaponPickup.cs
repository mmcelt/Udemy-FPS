using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
	#region Fields

	[SerializeField] string _theGun;

	bool _collected;

	#endregion

	#region MonoBehaviour Methods

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !_collected)
		{
			_collected = true;

			AudioManager.Instance.PlaySFX(4);
			PlayerController.Instance.AddGun(_theGun);

			Destroy(gameObject);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
