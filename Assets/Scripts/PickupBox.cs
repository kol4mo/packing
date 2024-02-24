using UnityEngine;

	[RequireComponent(typeof(AudioSource))]
	[RequireComponent(typeof(AudioSource))]
public class PickupBox : MonoBehaviour {
	[SerializeField] GameObject target;
	[SerializeField] Rigidbody2D rb;
	[SerializeField] AudioSource sPickup;
	[SerializeField] AudioSource sThud;
	public AudioClip pickup;
	public AudioClip thud;

	GameObject box;

	bool isCarried = false;
	bool canPlace = false;

	private void Awake() {
		//audio = GetComponent<AudioSource>();
	}

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
				if (!sPickup.isPlaying) {
					sPickup.PlayOneShot(pickup);
				}
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Place")) {
			canPlace = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Place")) {
			canPlace = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Place")) {
			canPlace = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Place")) {
			canPlace = false;
		}
	}

	private void Update() {
		if (isCarried) {
			box.transform.position = target.transform.position;
			if (box.TryGetComponent(out Rigidbody2D rb)) {
				if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) {
					rb.SetRotation(135);
				} else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) {
					rb.SetRotation(45);
				} else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) {
					rb.SetRotation(-45);
				} else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) {
					rb.SetRotation(-135);
				} else if (Input.GetKey(KeyCode.W)) {
					rb.SetRotation(180);
				} else if (Input.GetKey(KeyCode.S)) {
					rb.SetRotation(0);
				} else if (Input.GetKey(KeyCode.A)) {
					rb.SetRotation(-90);
				} else if (Input.GetKey(KeyCode.D)) {
					rb.SetRotation(90);
				}
			}
		}
		if (Input.GetKey(KeyCode.F) && isCarried == true && canPlace == true) {
			Vector3 newPosition = box.transform.position;
			isCarried = false;
			box.transform.position = newPosition;
			Vector3 temp = box.transform.position;
			temp.x = Mathf.Round(temp.x);
			temp.y = Mathf.Round(temp.y);
			box.transform.position = temp;
			sThud.PlayOneShot(thud);
			
		}
	}
}