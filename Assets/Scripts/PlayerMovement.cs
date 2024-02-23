using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] Rigidbody2D rb;
	[SerializeField] Animator animator;
	[SerializeField] float speed = 5f;

	private Vector2 movement;
	private void Start() {
	}

	void Update() {
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);

		//Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
		/*if (Input.GetAxisRaw("Horizontal") > 0) {
			rb.SetRotation(Vector3.Angle(Vector3.up, move) * -1);
		} else {
			rb.SetRotation(Vector3.Angle(Vector3.up, move));
		}*/
	}

	private void FixedUpdate() {
		rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
		
	}
}