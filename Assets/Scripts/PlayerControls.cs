using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour {

	public CharacterController characterController;

	[Header ("Movement")]
	public float speed = 6.0f;
	public float rotationSpeed = 20.0f;
	public float gravity = 10;
	private Vector2 moveInput = Vector2.zero;
	private Vector3 moveDirection = Vector3.zero;

	void Update () {
		moveDirection = moveInput.x * transform.right + moveInput.y * transform.forward;
		moveDirection *= speed;
		moveDirection.y -= gravity;
		characterController.Move (moveDirection * Time.deltaTime);
	}

	public void OnMove (InputValue value) {
		moveInput = value.Get<Vector2> ();
	}
}