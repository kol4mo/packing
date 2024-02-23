using UnityEngine;

public class PickupBox : MonoBehaviour {
	bool isCarried = false;
	[SerializeField] GameObject target;
	GameObject box;

	private void OnCollisionStay2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Box")) {
			if (Input.GetKey(KeyCode.E)) {
				box = collision.gameObject;
				isCarried = true;
				print("E pressed. Picked up box.");
			}
		}
	}

	private void OnTriggerStay2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Box")) {
			if (Input.GetKey(KeyCode.E)) {
				box = collision.gameObject;
				isCarried = true;
				print("E pressed. Picked up box.");
			}
		}
	}

	private void Update() {
		if (isCarried) {
			box.transform.position = target.transform.position;
		}
		if (Input.GetKey(KeyCode.F) && isCarried == true) {
			Vector3 newPosition = box.transform.position;
			box.transform.position = newPosition;
			isCarried = false;
		}
	}
}