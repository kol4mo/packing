using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour {
	[SerializeField] Rigidbody2D rb;
	[SerializeField] Animator animator;
	[SerializeField] float speed = 5f;
	[SerializeField] GameObject target;

	[SerializeField] AudioSource source;
	public AudioClip walk;

	bool isMoving = true;

	private Vector2 movement;

	private void Awake() {
	}

	void Update() {
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetFloat("Speed", movement.sqrMagnitude);

		if (movement.magnitude != 0) {
			isMoving = true;
		}
		else {
			isMoving = false;
		}
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

		if (isMoving) {
			if (!source.isPlaying) {
				source.PlayOneShot(walk);
			}
		}
		else {
			source.Stop();
		}
	}

	private void FixedUpdate() {
		movement.Normalize();
		rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
	}
}