using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Fields

	[SerializeField] float _moveSpeed;
	[SerializeField] CharacterController _controller;
	[SerializeField] Transform _theCamera;
	[SerializeField] float _mouseSensitivity;
	public bool _invertX, _invertY;

	Vector3 _moveInput;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		//move the player...
		_moveInput.x = Input.GetAxis("Horizontal");
		_moveInput.z = Input.GetAxis("Vertical");
		_moveInput *= _moveSpeed * Time.deltaTime;

		_controller.Move(_moveInput);

		//control camera & player rotation...
		Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * _mouseSensitivity;

		if (_invertX)
		{
			mouseInput.x = -mouseInput.x;
		}
		if (_invertY)
		{
			mouseInput.y = -mouseInput.y;
		}

		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

		_theCamera.rotation = Quaternion.Euler(_theCamera.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
