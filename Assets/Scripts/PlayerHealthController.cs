using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
	#region Fields

	public static PlayerHealthController Instance;

	[SerializeField] int _maxHealth, _currentHealth;
	[SerializeField] float _invincibleLength = 1f;

	float _invincibleCounter;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		//DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		_currentHealth = _maxHealth;
	}
	
	void Update() 
	{
		if (_invincibleCounter > 0)
			_invincibleCounter -= Time.deltaTime;
	}
	#endregion

	#region Public Methods

	public void DamagePlayer(int damageAmount)
	{
		if (_invincibleCounter <= 0)
		{
			_currentHealth -= damageAmount;
			_currentHealth = Mathf.Max(0, _currentHealth);

			if (_currentHealth == 0)
			{
				gameObject.SetActive(false);
			}

			_invincibleCounter = _invincibleLength;
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
