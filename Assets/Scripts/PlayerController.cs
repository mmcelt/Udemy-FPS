using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Fields

	[SerializeField] float _moveSpeed;
	[SerializeField] CharacterController _controller;

	Vector3 _moveInput;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		_moveInput.x = Input.GetAxis("Horizontal");
		_moveInput.z = Input.GetAxis("Vertical");
		_moveInput *= _moveSpeed * Time.deltaTime;

		_controller.Move(_moveInput);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
