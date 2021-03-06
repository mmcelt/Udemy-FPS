﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	#region Fields

	public static UIController Instance;

	public Slider _healthSlider;
	public Text _healthText, _ammoText;
	public Image _damagEffect, _fadePanel;
	public float _damageAlpha = 0.25f, _damageFadeSpeed = 2f, _fadeSpeed = 1f;
	[SerializeField] string _menuScene;
	public GameObject _pauseScreen;

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

	void Update()
	{
		if (_damagEffect.color.a != 0)
		{
			_damagEffect.color = new Color(_damagEffect.color.r, _damagEffect.color.g, _damagEffect.color.b, Mathf.MoveTowards(_damagEffect.color.a, 0f, _damageFadeSpeed * Time.deltaTime));
		}
		//I DID THIS IN THE LEVEL EXIT SCRIPT...
		//if (!GameManager.Instance._levelEnding)
		//{
		//	_fadePanel.color = new Color(_fadePanel.color.r, _fadePanel.color.g, _fadePanel.color.b, Mathf.MoveTowards(_fadePanel.color.a, 0f, _fadeSpeed * Time.deltaTime));
		//}
		//else
		//{
		//	_fadePanel.color = new Color(_fadePanel.color.r, _fadePanel.color.g, _fadePanel.color.b, Mathf.MoveTowards(_fadePanel.color.a, 1f, _fadeSpeed * Time.deltaTime));
		//}
	}
	#endregion

	#region Public Methods

	public void ShowDamage()
	{
		_damagEffect.color = new Color(_damagEffect.color.r, _damagEffect.color.g, _damagEffect.color.b, _damageAlpha);

	}
	#endregion

	#region Private Methods


	#endregion
}
