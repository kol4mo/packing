using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Truck : MonoBehaviour
{
	[SerializeField] FloatVariable score;
	[SerializeField] VoidEvent button;
	private List<GameObject> boxs = new List<GameObject>();

	public void Start() {
		button.Subscribe(pressButton);
	}

	public void pressButton() {
		int tempScore = 0;
		float multiplier = 1;
		for(int i = boxs.Count - 1; i >= 0; i--) {
			//boxs.Remove(box);
			tempScore += 5;
			multiplier += 0.2f;
			Destroy(boxs[i]);
		}
		boxs.Clear();
		score.value += tempScore * multiplier;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.GameObject().tag == "Box") {
			boxs.Add(collision.gameObject);
		}
	}
	private void OnTriggerExit2D(Collider2D other) {
		if (other.GameObject().tag == "Box") {
			boxs.Remove(other.gameObject);
		}
	}
}
