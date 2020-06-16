using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	#region Fields

	public static GameManager Instance;

	[SerializeField] float _waitAfterDying = 2f;

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
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods

	public void PlayerDied()
	{
		StartCoroutine(PlayerDiedRoutine());
	}
	#endregion

	#region Private Methods

	IEnumerator PlayerDiedRoutine()
	{
		PlayerHealthController.Instance.gameObject.SetActive(false);

		yield return new WaitForSeconds(_waitAfterDying);

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	#endregion
}
