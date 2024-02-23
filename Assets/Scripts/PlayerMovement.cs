using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] Rigidbody2D rb;
	[SerializeField] float speed;
	private void Start() {
	}

	void Update() {
		Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
		rb.velocity = Vector3.Normalize(move) * speed;
		if (Input.GetAxisRaw("Horizontal") > 0) {
			rb.SetRotation(Vector3.Angle(Vector3.up, move) * -1);
		} else {
			rb.SetRotation(Vector3.Angle(Vector3.up, move));
		}
	}
}