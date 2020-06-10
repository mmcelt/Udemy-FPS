using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	#region Fields

	[SerializeField] Transform _target;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void LateUpdate() 
	{
		transform.position = _target.position;
		transform.rotation = _target.rotation;
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
