using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] Rigidbody2D rb;
	[SerializeField] Animator animator;
	[SerializeField] float speed = 5f;
	[SerializeField] GameObject target;

	private Vector2 movement;
	private void Start() {
	}

	void Update() {
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetFloat("Speed", movement.sqrMagnitude);

		//Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
		/*if (Input.GetAxisRaw("Horizontal") > 0) {
			rb.SetRotation(Vector3.Angle(Vector3.up, move) * -1);
		} else {
			rb.SetRotation(Vector3.Angle(Vector3.up, move));
		}*/
		if (movement.magnitude > 0) {
			target.transform.position = gameObject.transform.position + new Vector3(movement.normalized.x, movement.normalized.y, 0);
		} else {
			target.transform.position = gameObject.transform.position + new Vector3(0, -1, 0);
		}
	}

	private void FixedUpdate() {
		rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
		
	}
}