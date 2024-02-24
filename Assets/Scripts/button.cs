using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class button : MonoBehaviour {
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip engine;

	[SerializeField] VoidEvent buttonpress;
	private void OnTriggerStay2D(Collider2D collision) {
		if (collision.tag == "pickup") {
			if (Input.GetKeyDown(KeyCode.E)) {
				buttonpress.RaiseEvent();
				audioSource.Play();
			}
		}
	}
}
