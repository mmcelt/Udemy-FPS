using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelExit : MonoBehaviour
{
	#region Fields

	[SerializeField] string _nextLevel;
	[SerializeField] float _waitToEndLevel;

	Image _fadePanelImage;
	bool _fadeOut, _fadeIn;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_fadePanelImage = UIController.Instance._fadePanel.GetComponent<Image>();
		_fadePanelImage.color = new Color(0, 0, 0, 1);
		StartCoroutine(StartLevelRoutine());
	}
	
	void Update() 
	{
		if (_fadeOut)
			FadeOut();

		if (_fadeIn)
			FadeIn();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			StartCoroutine(EndLevelRoutine());
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	IEnumerator EndLevelRoutine()
	{
		_fadeOut = true;
		GameManager.Instance._levelEnding = true;
		yield return new WaitUntil(() => _fadePanelImage.color.a == 1f);

		_fadeOut = false;
		GameManager.Instance._levelEnding = false;
		SceneManager.LoadScene(_nextLevel);
	}

	IEnumerator StartLevelRoutine()
	{
		_fadeIn = true;
		GameManager.Instance._levelEnding = true;
		yield return new WaitUntil(() => _fadePanelImage.color.a == 0f);

		_fadeIn = false;
		GameManager.Instance._levelEnding = false;
	}

	void FadeOut()
	{
		_fadePanelImage.color = new Color(0, 0, 0, Mathf.MoveTowards(_fadePanelImage.color.a, 1f, _waitToEndLevel * Time.deltaTime));
	}

	void FadeIn()
	{
		_fadePanelImage.color = new Color(0, 0, 0, Mathf.MoveTowards(_fadePanelImage.color.a, 0f, _waitToEndLevel * Time.deltaTime));
	}

	#endregion
}
