using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Truck : MonoBehaviour
{
	[SerializeField] FloatVariable score;
	[SerializeField] VoidEvent button;
	[SerializeField] GameObject floatScore;
	[SerializeField] GameObject pointPos;
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
			multiplier += 0.04f;
			Destroy(boxs[i]);
		}
		boxs.Clear();
		GameObject points = Instantiate(floatScore, pointPos.transform.position, Quaternion.identity);
		points.transform.GetChild(0).TryGetComponent(out TextMesh textMesh);
		textMesh.text = "$" + (tempScore * multiplier);
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
