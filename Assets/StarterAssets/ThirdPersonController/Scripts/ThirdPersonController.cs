﻿using UnityEngine;
using Cinemachine;
using System.Collections;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    //[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class ThirdPersonController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 2.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 5.335f;
		[Tooltip("How fast the character turns to face movement direction")]
		[Range(0.0f, 0.3f)]
		public float RotationSmoothTime = 0.12f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;
		[Tooltip("Frictiion when player not moving")]
		public float maxFriction = 10.0f;

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.50f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		public GroundCheck groundCheck = null;
		/*
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.28f;
		*/
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 70.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -30.0f;
		[Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
		public float CameraAngleOverride = 0.0f;
		[Tooltip("For locking the camera position on all axis")]
		public bool LockCameraPosition = false;

		// cinemachine
		private float _cinemachineTargetYaw;
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _animationBlend;
		private float _targetRotation = 0.0f;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

		// animation IDs
		private int _animIDSpeed;
		private int _animIDGrounded;
		private int _animIDJump;
		private int _animIDFreeFall;
		private int _animIDMotionSpeed;

		private Animator _animator;
		//private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;
		[Tooltip("Parent's Rigidbody")]
		public Rigidbody2D rbody2D;
		[Tooltip("Ground Check")]
		public Collider2D groundCheckCollider;
		[Tooltip("Player Physics Material")]
		public PhysicsMaterial2D playerPhysicsMaterial;

		private LayerMask slipperyObstacleColliderMask;


		private const float _threshold = 0.01f;

		private bool _hasAnimator;

		float targetSpeed;
		Vector3 inputDirection;
		float inputMagnitude;
		Vector2 origVelocity;

		//AddedForGame
		bool started=false;
		bool ended=false;
		bool cursorUnlocked=false;

		bool canMove;
		bool canJump;
		bool groundCheckColliderCouroutine;
		bool canClampVelocity;

		public static bool canApplyGravity;
		public static bool movementAllowed;

		private void Awake()
		{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
			_hasAnimator = TryGetComponent(out _animator);
			//_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();
			//rbody2D = GetComponent<Rigidbody2D>();

			slipperyObstacleColliderMask = LayerMask.GetMask("Foreground");

			canMove = false;
			canJump = false;
			groundCheckColliderCouroutine = false;
			canClampVelocity = false;

			canApplyGravity = true;
			movementAllowed = true;

			AssignAnimationIDs();

			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;

			fixFollowCamera();

			gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x, 180, gameObject.transform.rotation.eulerAngles.z);

			//gameObject.transform.rotation.eulerAngles = new Vector3(gameObject.transform.rotation.eulerAngles.x, 180, gameObject.transform.rotation.eulerAngles.z);
		}
		public void fixFollowCamera()
		{
			GameObject obj=  transform.GetChild(1).gameObject;

			if(obj.name== "CM FreeLook1")
			{
				obj.AddComponent<setFollowSettings>();
			}
		}

		private void Update()
		{

			_hasAnimator = TryGetComponent(out _animator);

			JumpAndGravity();
			GroundedCheck();
			Move();

			//Debug.Log("slimeCheck = " + slimeCheck);
	
			if (!cursorUnlocked && Keyboard.current[Key.Escape].wasPressedThisFrame)
			{
				cursorUnlocked = true;
				GetComponent<StarterAssetsInputs>().SetCursorLock(false);
			}
			if (cursorUnlocked && Mouse.current.leftButton.wasPressedThisFrame)
			{
				StartCoroutine(LockCursorAfter(1));
			}

			//collisionChecker("Links", LayerMask.GetMask("Default"));

			//Debug.Log("Links = " + collisionChecker("Links", LayerMask.GetMask("Default")));

			//Debug.Log("Handle = " + collisionChecker("Handle", LayerMask.GetMask("Default")));

			//Debug.Log("_verticalVelocity = " + _verticalVelocity);
		}

		private void FixedUpdate()
        {
			
			float currentHorizontalSpeed = new Vector3(rbody2D.velocity.x, 0.0f, 0.0f).magnitude;

			float speedOffset = 0.1f;
			
			// move the player
			//_controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			if (canMove)
            {
				if (movementAllowed)
                {
					playerPhysicsMaterial.friction = 0.1f;

					rbody2D.AddForce(inputDirection * _speed, ForceMode2D.Force);
				}
					
				//rbody2D.AddForce(inputDirection * (_speed * Time.deltaTime) * 50, ForceMode2D.Force);

				canClampVelocity = false;
			}
            else
            {
				if(collisionChecker("SlipperySurface", slipperyObstacleColliderMask))
                {
					playerPhysicsMaterial.friction = 0.1f;
				}
                else
                {
					if (playerPhysicsMaterial.friction < maxFriction)
					{
						playerPhysicsMaterial.friction = playerPhysicsMaterial.friction + Time.deltaTime * 5;
					}
				}

				
			}
 
			origVelocity = rbody2D.velocity;

			if (canJump)
			{
				
				if (movementAllowed)
				{
					rbody2D.velocity = new Vector2(rbody2D.velocity.x, 0.0f);
				}
				

				origVelocity.y = _verticalVelocity;
			}


			if(canClampVelocity)
            {
				origVelocity.x = Vector2.ClampMagnitude(rbody2D.velocity, targetSpeed).x;

				canClampVelocity = false;
			}
			else if (_input.move.x != 0)
			{
				origVelocity.x = Vector2.ClampMagnitude(rbody2D.velocity, targetSpeed).x;
			}



			/*
			if(_input.sprint && _input.move.x != 0)
            {
				origVelocity.x = Vector2.ClampMagnitude(rbody2D.velocity, SprintSpeed).x;
			}
            else if(_input.move.x != 0)
            {
				origVelocity.x = Vector2.ClampMagnitude(rbody2D.velocity, MoveSpeed).x;
			}
            else
            {
				origVelocity.x = Vector2.ClampMagnitude(rbody2D.velocity, MoveSpeed/4).x;
			}
			*/

			if (movementAllowed)
            {
				rbody2D.velocity = origVelocity;
			}


			//Debug.Log("targetSpeed = " + (targetSpeed));


			//Debug.Log("_speed = " + _speed);


			//Debug.Log("speed  = " + (inputDirection * (_speed * Time.deltaTime) * 50));


			//Debug.Log("velocity = " + rbody2D.velocity);

			//Debug.Log("targetSpeed = " + targetSpeed);


			//rbody2D.velocity = new Vector2(rbody2D.velocity.x, _verticalVelocity);

			/*
			if(nowJump)
            {
				JumpAndGravity();
			}
			*/

			//Debug.Log("Friction = " + playerPhysicsMaterial.friction);
		}


		private void LateUpdate()
		{

			CameraRotation();

			//gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
		}

		private void AssignAnimationIDs()
		{
			_animIDSpeed = Animator.StringToHash("Speed");
			_animIDGrounded = Animator.StringToHash("Grounded");
			_animIDJump = Animator.StringToHash("Jump");
			_animIDFreeFall = Animator.StringToHash("FreeFall");
			_animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
		}

		private bool collisionChecker(string objectTag, LayerMask layermask)
        {
			Collider2D parentCollider = gameObject.transform.parent.gameObject.GetComponent<Collider2D>();

			// Find the colliders that overlap this one
			Collider2D[] overlaps = new Collider2D[5];
			ContactFilter2D contactFilter = new ContactFilter2D();
			contactFilter.layerMask = layermask;
			parentCollider.OverlapCollider(contactFilter, overlaps);

			// Check if one of the overlapping colliders is on the "ground" layer
			foreach (Collider2D overlapCollider in overlaps)
			{
				if (overlapCollider != null)
				{
					// This line determines if the collider found is on a layer in the ground layer mask
					// sorry it is so math-heavy
					int match = contactFilter.layerMask.value & (int)Mathf.Pow(2, overlapCollider.gameObject.layer);
					if (match > 0)
					{
						//Debug.Log("match");

						if(overlapCollider.gameObject.CompareTag(objectTag))
                        {
							/*
							if (overlapCollider)
							{
								Debug.Log(overlapCollider.transform.name);
							}
							*/

							return true;
						}
						
					}
				}
			}

			return false;
        }

		private void GroundedCheck()
		{
			// set sphere position, with offset
			//Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			//Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

			// update animator if using character
			if (_hasAnimator)
			{
				_animator.SetBool(_animIDGrounded, grounded);
			}
		}

		private void CameraRotation()
		{
			/*
			// if there is an input and camera position is not fixed
			if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
			{
				//added for webGl
				float dampValue = 1;
#if UNITY_WEBGL && !UNITY_EDITOR
				dampValue = 0.25f;
#endif
				//
				_cinemachineTargetYaw += _input.look.x * Time.deltaTime  *dampValue; //dampvalue added for webGL
				_cinemachineTargetPitch += _input.look.y * Time.deltaTime * dampValue;//dampvalue added for webGL
			}
			*/

			// clamp our rotations so our values are limited 360 degrees
			_cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
			_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

			// Cinemachine will follow this target
			//CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);

			CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, 0.0f, 0.0f);

			//deformationSystem.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, deformationSystem.transform.rotation.y, deformationSystem.transform.rotation.z);




			//CinemachineCameraTarget.transform.rotation = Quaternion.Euler(0.0f, _cinemachineTargetYaw, 0.0f);
		}

		private void Move()
		{
			// set target speed based on move speed, sprint speed and if sprint is pressed
			targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_input.move.x == 0) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			//float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			
			//float currentHorizontalSpeed = new Vector3(rbody2D.velocity.x, 0.0f, 0.0f).magnitude;

			//float speedOffset = 0.1f;
			

			inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			/*

			
			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}
			*/

			_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);


			// normalise input direction
			//inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;
			inputDirection = new Vector3(_input.move.x, 0.0f, 0.0f).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.move.x != 0)
			{
				_targetRotation = Mathf.Atan2(inputDirection.x, 0) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
				float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);


				// rotate to face input direction relative to camera position
				transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);


			}



			//Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;


			//inputDirection = new Vector3(_input.move.x, 0.0f, 0.0f).normalized;

			if (_input.move.x != 0)
			{
				canMove = true;
			}
			else
			{
				if (canMove)
				{
					canClampVelocity = true;
				}

				canMove = false;
			}
			


			/*
			Vector2 origVelocity;
			
			// move the player
			//_controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

			
			inputDirection = new Vector3(_input.move.x, 0.0f, 0.0f).normalized;

			rbody2D.AddForce(inputDirection * _speed, ForceMode2D.Force);

			origVelocity = rbody2D.velocity;

			origVelocity.x = Vector2.ClampMagnitude(rbody2D.velocity, targetSpeed).x;

			rbody2D.velocity = origVelocity;
			*/


			// update animator if using character
			if (_hasAnimator)
			{
				_animator.SetFloat(_animIDSpeed, _animationBlend);
				_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
			}

		
		}

		private void JumpAndGravity()
		{
			if (grounded)
			{
				//Debug.Log("grounded");

				// reset the fall timeout timer
				_fallTimeoutDelta = FallTimeout;

				// update animator if using character
				if (_hasAnimator)
				{
					_animator.SetBool(_animIDJump, false);
					_animator.SetBool(_animIDFreeFall, false);
				}

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				// Jump
				if (_input.jump && _jumpTimeoutDelta <= 0.0f)
				{
					//Debug.Log("_jumpTimeoutDelta <= 0.0f");

					canJump = true;

					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

					//rbody2D.velocity = new Vector2(rbody2D.velocity.x, _verticalVelocity);

					// update animator if using character
					if (_hasAnimator)
					{
						_animator.SetBool(_animIDJump, true);
					}

					if(!groundCheckColliderCouroutine)
                    {
						StartCoroutine(groundCheckColliderDeactivate(0.2f));
					}
				}
                else
                {
					canJump = false;
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{

				//Debug.Log("not grounded");

				// reset the jump timeout timer
				_jumpTimeoutDelta = JumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}
				else
				{
					// update animator if using character
					if (_hasAnimator)
					{
						_animator.SetBool(_animIDFreeFall, true);
					}
				}

				/*
				if (!jumpWaitCoroutine)
				{
					// if we are not grounded, do not jump
					_input.jump = false;
				}
				else
                {
					_input.jump = true;
				}
				*/
				

				if(canApplyGravity)
                {
					// if we are not grounded, do not jump
					_input.jump = false;
				}

				
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				//canJump = true;

				if(canApplyGravity)
                {
					_verticalVelocity += Gravity * Time.deltaTime;
				}
                else
                {
					//_verticalVelocity = 5.0f;

					if(movementAllowed)
                    {
						_verticalVelocity = rbody2D.velocity.y;
					}
                    else
                    {
						if (movementAllowed)
                        {
							rbody2D.velocity = new Vector2(rbody2D.velocity.x, 0.0f);
						}
							

						_verticalVelocity = 8f;
					}
					
				}
				
				

				//rbody2D.velocity = new Vector2(rbody2D.velocity.x, _verticalVelocity);
			}
            else
            {
				//canJump = false;
			}
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		/*
		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;
			
			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}
		*/


		//Added ForGame
		public void SetStarted(bool b)
        {
			started = b;
			if(b)
				GetComponent<StarterAssetsInputs>().SetCursorLock(true);
        }
		public void SetEnded(bool b)
		{
			ended = b;
			if(b)
				GetComponent<StarterAssetsInputs>().SetCursorLock(false);

		}
		
		IEnumerator LockCursorAfter(float x)
        {
			yield return new WaitForSeconds(x);
			cursorUnlocked = false;
			GetComponent<StarterAssetsInputs>().SetCursorLock(true);
			
		}

		public bool grounded
		{
			get
			{
				if (groundCheck != null)
				{
					return groundCheck.CheckGrounded();
				}
				else
				{
					return false;
				}
			}
		}

		IEnumerator groundCheckColliderDeactivate(float secs)
		{
			groundCheckColliderCouroutine = true;

			groundCheckCollider.GetComponent<Collider2D>().enabled = false;

			//Debug.Log("Deactivated");

			yield return new WaitForSeconds(secs);

			//Debug.Log("Activated");

			groundCheckCollider.GetComponent<Collider2D>().enabled = true;

			groundCheckColliderCouroutine = false;
		}

	}
}