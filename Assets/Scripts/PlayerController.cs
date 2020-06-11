using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Fields

	[SerializeField] float _moveSpeed, _gravityModifier, _jumpPower, _runSpeed = 12f;
	[SerializeField] CharacterController _controller;
	[SerializeField] Transform _theCamera, _groundCheckPoint;
	[SerializeField] float _mouseSensitivity;
	[SerializeField] LayerMask _whatIsGround;

	public bool _invertX, _invertY;

	[Header("Shooting")]
	[SerializeField] GameObject _bulletPrefab;
	[SerializeField] Transform _firePoint;

	Vector3 _moveInput;
	bool _canJump, _canDoubleJump;

	Animator _anim;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_anim = GetComponent<Animator>();
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

		if (Input.GetKey(KeyCode.LeftShift))
		{
			_moveInput *= _runSpeed;
		}
		else
		{
			_moveInput *= _moveSpeed;
		}

		_moveInput.y = yStore;

		_moveInput.y += Physics.gravity.y * _gravityModifier * Time.deltaTime;

		if (_controller.isGrounded)
		{
			_moveInput.y = Physics.gravity.y * Time.deltaTime* _gravityModifier;
		}

		_canJump = Physics.OverlapSphere(_groundCheckPoint.position, 0.25f, _whatIsGround).Length > 0;

		if (_canJump)
		{
			_canDoubleJump = true;	//CHANGED FROM FALSE TO TRUE
		}

		//handle jumping
		if (Input.GetKeyDown(KeyCode.Space) && _canJump)
		{
			//_canDoubleJump = true;
			_moveInput.y = _jumpPower;
		}
		else if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
		{
			_moveInput.y = _jumpPower;
			_canDoubleJump = false;
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


		//shooting
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if (Physics.Raycast(_theCamera.position, _theCamera.forward, out hit, 50f))
			{
				if(Vector3.Distance(_theCamera.position,hit.point) > 2f)
					_firePoint.LookAt(hit.point);
			}
			else
			{
				_firePoint.LookAt(_theCamera.position + (_theCamera.forward * 50f));
			}
			Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
		}

		_anim.SetFloat("moveSpeed", _moveInput.magnitude);
		_anim.SetBool("onGround", _canJump);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
