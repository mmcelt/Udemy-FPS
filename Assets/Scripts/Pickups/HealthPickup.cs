using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
	#region Fields

	[SerializeField] int _healAmount;

	bool _collected;

	#endregion

	#region MonoBehaviour Methods

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !_collected)
		{
			_collected = true;

			AudioManager.Instance.PlaySFX(5);
			PlayerHealthController.Instance.HealPlayer(_healAmount);

			Destroy(gameObject);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
