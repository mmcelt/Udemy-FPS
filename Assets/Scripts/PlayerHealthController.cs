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
		UpdateHealthUI();
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
			AudioManager.Instance.PlaySFX(7);
			_currentHealth -= damageAmount;
			_currentHealth = Mathf.Max(0, _currentHealth);

			UIController.Instance.ShowDamage();

			UpdateHealthUI();

			if (_currentHealth == 0)
			{
				GameManager.Instance.PlayerDied();
				AudioManager.Instance.StopBGM();
				AudioManager.Instance.StopSFX(7);
				AudioManager.Instance.PlaySFX(6);
			}

			_invincibleCounter = _invincibleLength;
		}
	}

	public void HealPlayer(int healAmount)
	{
		_currentHealth += healAmount;
		_currentHealth = Mathf.Min(_currentHealth, _maxHealth);

		UpdateHealthUI();
	}
	#endregion

	#region Private Methods

	void UpdateHealthUI()
	{
		UIController.Instance._healthSlider.maxValue = _maxHealth;
		UIController.Instance._healthSlider.value = _currentHealth;
		UIController.Instance._healthText.text = "HEALTH: " + _currentHealth + "/" + _maxHealth;
	}
	#endregion
}
