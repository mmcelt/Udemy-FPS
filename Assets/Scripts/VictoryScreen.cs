using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _headerText, _messageText, _returnButton;
	[SerializeField] Image _fadePanel;
	[SerializeField] float _fadeTime, _headerTime, _messageTime, _buttonTime;
	[SerializeField] string _mainMenuScene;
	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		StartCoroutine(ActivateVictoryScreenRoutine());
	}
	
	void Update() 
	{
		_fadePanel.color = new Color(0, 0, 0, Mathf.MoveTowards(_fadePanel.color.a, 0, _fadeTime * Time.deltaTime));
	}
	#endregion

	#region Public Methods

	public void ReturnToMainMenuButtonClicked()
	{
		SceneManager.LoadScene(_mainMenuScene);
	}
	#endregion

	#region Private Methods

	IEnumerator ActivateVictoryScreenRoutine()
	{
		yield return new WaitUntil(() => _fadePanel.color.a == 0);
		Cursor.lockState = CursorLockMode.None;
		yield return new WaitForSeconds(_headerTime);
		_headerText.SetActive(true);
		yield return new WaitForSeconds(_messageTime);
		_messageText.SetActive(true);
		yield return new WaitForSeconds(_buttonTime);
		_returnButton.SetActive(true);
	}
	#endregion
}
