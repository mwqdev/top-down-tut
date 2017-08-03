using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float movementSpeed;
	private Rigidbody playerBody;

	private Vector3 moveInput;
	private Vector3 moveVelocity;

	private Camera mainCamera;

	public GunController playerGun;

	public bool useController;

	// Use this for initialization
	void Start ()
	{
		playerBody = GetComponent<Rigidbody> ();
		mainCamera = FindObjectOfType<Camera> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0f, Input.GetAxisRaw ("Vertical"));
		moveVelocity = moveInput * movementSpeed;

		// Mouse rotation
		if (!useController) {
			Ray cameraRay = mainCamera.ScreenPointToRay (Input.mousePosition);
			Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
			float rayLength;

			if (groundPlane.Raycast (cameraRay, out rayLength)) {
				Vector3 pointToLook = cameraRay.GetPoint (rayLength);
				Debug.DrawLine (cameraRay.origin, pointToLook, Color.blue);

				transform.LookAt (new Vector3 (pointToLook.x, transform.position.y, pointToLook.z));
			}

			if (Input.GetMouseButtonDown (0)) {
				playerGun.isFiring = true;
			}

			if (Input.GetMouseButtonUp (0)) {
				playerGun.isFiring = false;
			}
		}

		// Stick rotation
		if (useController) {
			Vector3 playerDirection = Vector3.right * Input.GetAxisRaw ("RStickHoriz") + Vector3.forward * -Input.GetAxisRaw ("RStickVert");
			if (playerDirection.sqrMagnitude > 0.0f) {
				transform.rotation = Quaternion.LookRotation (playerDirection, Vector3.up);
			}
			if (Input.GetKeyDown (KeyCode.Joystick1Button5)) {
				playerGun.isFiring = true;
			}
			if (Input.GetKeyUp (KeyCode.Joystick1Button5)) {
				playerGun.isFiring = false;
			}
		}
	}

	// Happens every 0.02 seconds
	void FixedUpdate ()
	{
		playerBody.velocity = moveVelocity;
	}

}
