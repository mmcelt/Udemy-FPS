using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: MonoBehaviour
{
	#region Fields

	public static AudioManager Instance;

	public AudioSource _bgm;
	public AudioSource[] _soundEfects;

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
	#endregion

	#region Public Methods

	public void StopBGM()
	{
		_bgm.Stop();
	}

	public void PlaySFX(int sfxIndex)
	{
		StopSFX(sfxIndex);
		_soundEfects[sfxIndex].Play();
	}

	public void StopSFX(int sfxIndex)
	{
		_soundEfects[sfxIndex].Stop();
	}
	#endregion

	#region Private Methods


	#endregion
}
