using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	#region Fields

	[SerializeField] int _explosionDamage = 25;
	[SerializeField] bool _damageEnemy, _damagePlayer;

	#endregion

	#region MonoBehaviour Methods

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy") && _damageEnemy)
		{
			if(other.GetComponent<EnemyHealthController>())
				other.GetComponent<EnemyHealthController>().DamageEnemy(_explosionDamage);
		}

		if (other.CompareTag("Player") && _damagePlayer)
		{
			PlayerHealthController.Instance.DamagePlayer(_explosionDamage);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
