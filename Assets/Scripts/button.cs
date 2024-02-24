using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
	[SerializeField] VoidEvent buttonpress;
	private void OnTriggerStay2D(Collider2D collision) {
		if (collision.tag == "pickup") {
			if (Input.GetKeyDown(KeyCode.E)) {
				buttonpress.RaiseEvent();
			}
		}
	}
}
