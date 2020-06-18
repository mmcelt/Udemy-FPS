using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	#region Fields

	public static CameraController Instance;

	[SerializeField] Transform _target;
	[SerializeField] float _zoomSpeed = 1f;
	[SerializeField] Camera _theCamera;

	float _startFOV, _targetFOV;

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
		_startFOV = _theCamera.fieldOfView;
		_targetFOV = _startFOV;
	}
	
	void LateUpdate() 
	{
		if(_target != null)
		{
			transform.position = _target.position;
			transform.rotation = _target.rotation;
		}

		_theCamera.fieldOfView = Mathf.Lerp(_theCamera.fieldOfView, _targetFOV, _zoomSpeed * Time.deltaTime);
	}
	#endregion

	#region Public Methods

	public void ZoomIn(float newZoom)
	{
		_targetFOV = newZoom;
	}

	public void ZoomOut()
	{
		_targetFOV = _startFOV;
	}
	#endregion

	#region Private Methods


	#endregion
}
