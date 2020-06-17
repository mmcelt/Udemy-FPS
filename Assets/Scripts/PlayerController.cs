﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Fields

	public static PlayerController Instance;

	[SerializeField] float _moveSpeed, _gravityModifier, _jumpPower, _runSpeed = 12f;
	[SerializeField] CharacterController _controller;
	[SerializeField] Transform _theCamera, _groundCheckPoint;
	[SerializeField] float _mouseSensitivity;
	[SerializeField] LayerMask _whatIsGround, _shootingRayLayers;

	public bool _invertX, _invertY;

	[Header("Shooting")]
	[SerializeField] Transform _adsPoint;
	[SerializeField] Transform _gunHolder;
	[SerializeField] float _adsSpeed = 2f;

	//[SerializeField] GameObject _bulletPrefab;
	//[SerializeField] Transform _firePoint;
	public Gun _activeGun;
	public List<Gun> _allGuns = new List<Gun>();
	public List<Gun> _unlockableGuns = new List<Gun>();

	[SerializeField] int _currentGun;

	Vector3 _moveInput;
	Vector3 _gunStartPos;
	bool _canJump, _canDoubleJump;

	Animator _anim;

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
		_anim = GetComponent<Animator>();
		_gunStartPos = _gunHolder.localPosition;
		_currentGun--;
		SwitchGun();
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


		//shooting - single shot...
		if (Input.GetMouseButtonDown(0) && _activeGun._fireCounter <= 0)
		{
			RaycastHit hit;
			if (Physics.Raycast(_theCamera.position, _theCamera.forward, out hit, 50f, _shootingRayLayers))
			{
				if(Vector3.Distance(_theCamera.position,hit.point) > 2f)
					_activeGun._firePoint.LookAt(hit.point);
			}
			else
			{
				_activeGun._firePoint.LookAt(_theCamera.position + (_theCamera.forward * 50f));
			}
			//Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
			FireShot();
		}
		//auto fire...
		if (Input.GetMouseButton(0) && _activeGun._canAutoFire)
		{
			if (_activeGun._fireCounter <= 0)
				FireShot();
		}

		//switch gun...
		if (Input.GetKeyDown(KeyCode.Tab))
			SwitchGun();

		//Aiming - zoom in...
		if (Input.GetMouseButtonDown(1))
		{
			CameraController.Instance.ZoomIn(_activeGun._zoomAmount);
		}
		//center the gun
		if (Input.GetMouseButton(1))
		{
			_gunHolder.position = Vector3.MoveTowards(_gunHolder.position, _adsPoint.position, _adsSpeed * Time.deltaTime);
		}
		else
		{
			_gunHolder.localPosition = Vector3.MoveTowards(_gunHolder.localPosition, _gunStartPos, _adsSpeed * Time.deltaTime);
		}
		//Aiming - zoom out...
		if (Input.GetMouseButtonUp(1))
		{
			CameraController.Instance.ZoomOut();
		}

		_anim.SetFloat("moveSpeed", _moveInput.magnitude);
		_anim.SetBool("onGround", _canJump);
	}
	#endregion

	#region Public Methods

	public void AddGun(string gunToAdd)
	{
		bool gunUnlocked = false;

		if (_unlockableGuns.Count > 0)
		{
			for (int i=0; i<_unlockableGuns.Count; i++)
			{
				if (_unlockableGuns[i]._gunName == gunToAdd)
				{
					gunUnlocked = true;
					_allGuns.Add(_unlockableGuns[i]);
					_unlockableGuns.RemoveAt(i);
					i = _unlockableGuns.Count;
				}
			}
		}

		if (gunUnlocked)
		{
			_currentGun = _allGuns.Count - 2;
			SwitchGun();
		}
	}
	#endregion

	#region Private Methods

	void FireShot()
	{
		if(_activeGun._currentAmmo > 0)
		{
			_activeGun._currentAmmo--;

			UpdateAmmoUI();

			Instantiate(_activeGun._bullet, _activeGun._firePoint.position, _activeGun._firePoint.rotation);

			_activeGun._fireCounter = _activeGun._fireRate;
		}
	}

	void UpdateAmmoUI()
	{
		UIController.Instance._ammoText.text = "AMMO: " + _activeGun._currentAmmo;
	}

	void SwitchGun()
	{
		_activeGun.gameObject.SetActive(false);
		_currentGun++;

		_currentGun = ( _currentGun >= _allGuns.Count) ? 0 : _currentGun;

		_activeGun = _allGuns[_currentGun];
		_activeGun.gameObject.SetActive(true);
		UpdateAmmoUI();
	}
	#endregion
}
