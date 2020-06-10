using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Fields

	[SerializeField] float _moveSpeed, _gravityModifier, _jumpPower;
	[SerializeField] CharacterController _controller;
	[SerializeField] Transform _theCamera, _groundCheckPoint;
	[SerializeField] float _mouseSensitivity;
	[SerializeField] LayerMask _whatIsGround;

	public bool _invertX, _invertY;

	Vector3 _moveInput;
	bool _canJump;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		//move the player...
		//_moveInput.x = Input.GetAxis("Horizontal");
		//_moveInput.z = Input.GetAxis("Vertical");
		//_moveInput *= _moveSpeed * Time.deltaTime;

		//store y velocity
		float yStore = _moveInput.y;

		Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
		Vector3 horizMove = transform.right * Input.GetAxis("Horizontal");

		_moveInput = vertMove + horizMove;
		_moveInput.Normalize();
		_moveInput *= _moveSpeed;

		_moveInput.y = yStore;

		_moveInput.y += Physics.gravity.y * _gravityModifier * Time.deltaTime;

		if (_controller.isGrounded)
		{
			_moveInput.y = Physics.gravity.y * Time.deltaTime* _gravityModifier;
		}

		_canJump = Physics.OverlapSphere(_groundCheckPoint.position, 0.25f, _whatIsGround).Length > 0;

		//handle jumping
		if (Input.GetKeyDown(KeyCode.Space) && _canJump)
		{
			_moveInput.y = _jumpPower;
		}

		_controller.Move(_moveInput * Time.deltaTime);

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
