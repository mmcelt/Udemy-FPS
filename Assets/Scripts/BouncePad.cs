using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
	#region Fields

	[SerializeField] float _bounceForce;

	#endregion

	#region MonoBehaviour Methods

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerController.Instance.Bounce(_bounceForce);
			AudioManager.Instance.PlaySFX(0);
		}
	}	
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
