using System.Collections.Generic;
using UnityEngine;

	[RequireComponent(typeof(AudioSource))]
	[RequireComponent(typeof(AudioSource))]
public class PickupBox : MonoBehaviour {
	[SerializeField] GameObject target;
	[SerializeField] Rigidbody2D rb;
	[SerializeField] AudioSource sPickup;
	[SerializeField] AudioSource sThud;
	[SerializeField] int x;
	[SerializeField] int y;
	[SerializeField] Transform topleftCorner;
	List<List<bool>> isthere;
	public AudioClip pickup;
	public AudioClip thud;

	GameObject box;

	bool isCarried = false;
	bool canPlace = false;

	private void Awake() {
		//audio = GetComponent<AudioSource>();
		isthere = new List<List<bool>>();
		for (int x = 0; x < this.x; x++) {
			isthere.Add(new List<bool>());
			for (int y = 0; y < this.y; y++) {
				isthere[x].Add(false);
			}
		}
		
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
		if (Input.GetKey(KeyCode.F) && isCarried == true && canPlace == true && box.TryGetComponent(out PathFollower pf)) {
			Vector3 temp;
			bool allgood = true;
			foreach (Transform t in pf.positions) {
				temp = box.transform.position;
				temp.x = Mathf.Round(temp.x);
				temp.y = Mathf.Round(temp.y);
				temp = temp- topleftCorner.position;
				if (isthere[(int)temp.x][(int)temp.y]) {
					allgood = false;
				}
			}


			if (allgood) {
				foreach (Transform t in pf.positions) {
					temp = box.transform.position;
					temp.x = Mathf.Round(temp.x);
					temp.y = Mathf.Round(temp.y);
					temp = temp - topleftCorner.position;
					isthere[(int)temp.x][(int)temp.y] = true;
				}
				//box.transform.position = newPosition;
				temp = box.transform.position;
				temp.x = Mathf.Round(temp.x);
				temp.y = Mathf.Round(temp.y);
				box.transform.position = temp;
				sThud.PlayOneShot(thud);
				isCarried = false;
			}
		}
	}
}