using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
	#region Fields

	[SerializeField] int _currentHealth;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods

	public void DamageEnemy(int damageAmount)
	{
		_currentHealth -= damageAmount;
		_currentHealth = Mathf.Max(0, _currentHealth);

		if (_currentHealth == 0)
			Destroy(gameObject);
	}
	#endregion

	#region Private Methods


	#endregion
}
