using UnityEngine;

public class PickupBox : MonoBehaviour {
	bool isCarried = false;
	[SerializeField] GameObject target;

	private void OnCollisionStay2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			if (Input.GetKeyDown(KeyCode.E)) {
				isCarried = true;
				print("E pressed. Picked up box.");
			}
		}
	}

	private void Update() {
		if (isCarried == true) {
			transform.position = target.transform.position;
		}
		if (Input.GetKeyDown(KeyCode.F) && (isCarried == true)) {
		Vector3 newPosition = transform.position;
		transform.position = newPosition;
		isCarried = false;
		}
	}
}