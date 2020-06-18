using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: MonoBehaviour
{
	#region Fields

	public static AudioManager Instance;

	[SerializeField] AudioSource _bgm;

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
		
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods

	public void StopBGM()
	{
		_bgm.Stop();
	}
	#endregion

	#region Private Methods


	#endregion
}
