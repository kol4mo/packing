using UnityEngine;

	[RequireComponent(typeof(AudioSource))]
	[RequireComponent(typeof(AudioSource))]
public class PickupBox : MonoBehaviour {
	bool isCarried = false;
	[SerializeField] GameObject target;
	[SerializeField] Rigidbody2D rb;
	AudioSource audio;
	public AudioClip pickup;
	public AudioClip thud;
	GameObject box;

	private void Awake() {
		audio = GetComponent<AudioSource>();
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
				if (!audio.isPlaying) {
					audio.PlayOneShot(pickup);
				}
			}
		}
	}

	private void Update() {
		if (isCarried) {
			box.transform.position = target.transform.position;
			Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
			if (Input.GetAxisRaw("Horizontal") > 0) {

				//rb.SetRotation(Vector3.Angle(Vector3.up, move) * -1);
			}
			else {
				//rb.SetRotation(Vector3.Angle(Vector3.up, move));
			}
		}
		if (Input.GetKey(KeyCode.F) && isCarried == true) {
			Vector3 newPosition = box.transform.position;
			isCarried = false;
			//box.transform.position = newPosition;
			Vector3 temp = box.transform.position;
			temp.x = Mathf.Round(temp.x);
			temp.y = Mathf.Round(temp.y);
			box.transform.position = temp;
			audio.PlayOneShot(thud);
		}
	}
}