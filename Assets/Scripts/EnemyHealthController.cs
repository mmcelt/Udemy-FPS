﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
	#region Fields

	[SerializeField] int _currentHealth;
	[SerializeField] EnemyController _theEC;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	#endregion

	#region Public Methods

	public void DamageEnemy(int damageAmount)
	{
		_currentHealth -= damageAmount;
		_currentHealth = Mathf.Max(0, _currentHealth);

		if (_theEC != null)
		{
			_theEC.GotShot();
		}

		if (_currentHealth == 0)
		{
			Destroy(gameObject);
			AudioManager.Instance.PlaySFX(2);
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
