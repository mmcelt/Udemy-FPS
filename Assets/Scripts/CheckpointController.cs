using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
	#region Fields

	public string _cpName;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_cp"))
		{
			if(PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_cp") == _cpName)
			{
				PlayerController.Instance.transform.position = transform.position;
			}
		}
	}
	
	void Update() 
	{
		if(Input.GetKeyDown(KeyCode.L))
			PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", "");
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", _cpName);
			AudioManager.Instance.PlaySFX(1);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
